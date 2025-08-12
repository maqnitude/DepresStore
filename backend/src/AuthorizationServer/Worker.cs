using DepresStore.Modules.Identity.Domain.Entities;
using DepresStore.Modules.Identity.Infrastructure.Data;
using DepresStore.Shared.Kernel.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace DepresStore.AuthorizationServer
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        private const string AdminEmail = "admin@example.com";
        private const string AdminPassword = "Admin@123";

        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var context = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

            // Apply any pending migrations
            if ((await context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            {
                await context.Database.MigrateAsync(cancellationToken);
            }

            // Seed user roles

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            if (!await roleManager.RoleExistsAsync(RoleConstants.Administrator))
            {
                await roleManager.CreateAsync(new(RoleConstants.Administrator));
            }

            if (!await roleManager.RoleExistsAsync(RoleConstants.Customer))
            {
                await roleManager.CreateAsync(new(RoleConstants.Customer));
            }

            // Seed admin

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (await userManager.FindByEmailAsync(AdminEmail) is null)
            {
                var adminUser = new User
                {
                    FirstName = "John",
                    LastName = "Admin",
                    UserName = AdminEmail,
                    Email = AdminEmail
                };

                var result = await userManager.CreateAsync(adminUser, AdminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, RoleConstants.Administrator);
                }
            }

            // Seed OpenIddict client applications

            var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await applicationManager.FindByClientIdAsync("postman", cancellationToken) is null)
            {
                await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "postman",
                    ClientSecret = "postman-secret",
                    DisplayName = "Postman",
                    RedirectUris =
                    {
                        new Uri("https://oauth.pstmn.io/v1/callback")
                    },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,

                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,

                        OpenIddictConstants.Permissions.Prefixes.Scope + "api",

                        OpenIddictConstants.Permissions.ResponseTypes.Code
                    }
                }, cancellationToken);
            }

            if (await applicationManager.FindByClientIdAsync("storefront", cancellationToken) is null)
            {
                await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "storefront",
                    ClientSecret = "storefront-secret",
                    DisplayName = "Storefront",
                    RedirectUris =
                    {
                        new Uri("https://localhost:7002/callback/login/local")
                    },
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://localhost:7002/callback/logout/local")
                    },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.Endpoints.EndSession,

                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,

                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,

                        OpenIddictConstants.Permissions.ResponseTypes.Code
                    },
                    Requirements =
                    {
                        OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                    }
                }, cancellationToken);
            }

            if (await applicationManager.FindByClientIdAsync("backoffice", cancellationToken) is null)
            {
                await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "backoffice",
                    DisplayName = "Backoffice",
                    RedirectUris =
                    {
                        new Uri("https://localhost:3000/login-callback")
                    },
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://localhost:3000/logout-callback")
                    },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.Endpoints.EndSession,

                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,

                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,

                        OpenIddictConstants.Permissions.ResponseTypes.Code
                    },
                    Requirements =
                    {
                        OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                    }
                }, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
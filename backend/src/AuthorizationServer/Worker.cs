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
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
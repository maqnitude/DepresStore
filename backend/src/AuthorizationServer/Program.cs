using DepresStore.AuthorizationServer;
using DepresStore.Modules.Identity.Domain.Entities;
using DepresStore.Modules.Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using OpenIddict.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppIdentityDbContext>(dbContextOptions =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    dbContextOptions.UseSqlServer(connectionString, sqlServerOptions =>
    {
        sqlServerOptions.MigrationsHistoryTable(
            HistoryRepository.DefaultTableName,
            AppIdentityDbContext.Schema);
    });

    dbContextOptions.UseOpenIddict();
});

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();

// Configure application cookie used by Identity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
            .UseDbContext<AppIdentityDbContext>();
    })
    .AddServer(options =>
    {
        options
            .AllowAuthorizationCodeFlow()
                .RequireProofKeyForCodeExchange();

        options
            .SetAuthorizationEndpointUris("/connect/authorize")
            .SetTokenEndpointUris("/connect/token")
            .SetEndSessionEndpointUris("/connect/logout");

        options
            .RegisterScopes(
                OpenIddictConstants.Scopes.OpenId,
                OpenIddictConstants.Scopes.Email,
                OpenIddictConstants.Scopes.Profile,
                OpenIddictConstants.Scopes.Roles,
                "api");

        options
            .AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate()
            .DisableAccessTokenEncryption();

        options
            .UseAspNetCore()
            .EnableAuthorizationEndpointPassthrough()
            .EnableTokenEndpointPassthrough()
            .EnableEndSessionEndpointPassthrough();
    });

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();

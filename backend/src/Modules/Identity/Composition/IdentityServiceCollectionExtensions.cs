using DepresStore.Modules.Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace DepresStore.Modules.Identity.Composition
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityModule(
            this IServiceCollection services,
            string? connectionString)
        {
            services.AddDbContext<AppIdentityDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        AppIdentityDbContext.Schema);
                });
            });

            return services;
        }
    }
}
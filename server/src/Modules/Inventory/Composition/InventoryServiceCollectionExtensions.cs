using DepresStore.Modules.Catalog.IntegrationEvents;
using DepresStore.Modules.Inventory.Application.Features.Stocks.IntegrationEventHandlers;
using DepresStore.Modules.Inventory.Infrastructure.Data;
using DepresStore.Shared.Kernel.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace DepresStore.Modules.Inventory.Composition
{
    public static class InventoryServiceCollectionExtensions
    {
        public static IServiceCollection AddInventoryModule(
            this IServiceCollection services,
            string? connectionString)
        {
            services.AddDbContext<InventoryDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName, InventoryDbContext.Schema);
                });
            });

            services.AddIntegrationEventHandlers();

            return services;
        }

        private static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
        {
            services.AddScoped<IEventHandler<ProductCreated>, ProductCreatedEventHandler>();

            return services;
        }
    }
}
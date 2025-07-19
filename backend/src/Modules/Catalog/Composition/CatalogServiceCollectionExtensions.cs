using DepresStore.Modules.Catalog.Application.Features.Products.Commands;
using DepresStore.Modules.Catalog.Application.Features.Products.DomainEventHandlers;
using DepresStore.Modules.Catalog.Application.Features.Products.Queries;
using DepresStore.Modules.Catalog.Domain.Events;
using DepresStore.Modules.Catalog.Infrastructure;
using DepresStore.Modules.Catalog.Infrastructure.Data;
using DepresStore.Shared.Kernel.Application;
using DepresStore.Shared.Kernel.Application.Models;
using DepresStore.Shared.Kernel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace DepresStore.Modules.Catalog.Composition
{
    public static class CatalogServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogModule(
            this IServiceCollection services,
            string? connectionString)
        {
            services.AddDbContext<CatalogDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        CatalogDbContext.Schema);
                });
            });

            services.AddQueryHandlers();
            services.AddCommandHandlers();

            services.AddKeyedScoped<IDomainEventDispatcher, CatalogDomainEventDispatcher>("catalog");
            services.AddDomainEventHandlers();

            return services;
        }

        private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            services.AddScoped<IQueryHandler<GetProductsQuery, PaginatedList<ProductDto>>, GetProductsQueryHandler>();

            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateProductCommand>, UpdateProductCommandHandler>();

            return services;
        }

        private static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
        {
            services.AddScoped<IEventHandler<ProductNameChanged>, ProductNameChangedEventHandler>();

            return services;
        }
    }
}
using DepresStore.Modules.Catalog.Application.Features.Products.Commands;
using DepresStore.Modules.Catalog.Application.Features.Products.DomainEventHandlers;
using DepresStore.Modules.Catalog.Application.Features.Products.Queries;
using DepresStore.Modules.Catalog.Domain.Events;
using DepresStore.Modules.Catalog.Infrastructure;
using DepresStore.Shared.Kernel.Application;
using DepresStore.Shared.Kernel.Application.Models;
using DepresStore.Shared.Kernel.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace DepresStore.Modules.Catalog.Composition
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services)
        {
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
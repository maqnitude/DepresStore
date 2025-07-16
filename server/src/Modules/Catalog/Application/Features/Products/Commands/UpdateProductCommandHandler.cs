using DepresStore.Modules.Catalog.Domain.Entities;
using DepresStore.Modules.Catalog.Domain.ValueObjects;
using DepresStore.Shared.Kernel.Application;
using DepresStore.Shared.Kernel.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DepresStore.Modules.Catalog.Application.Features.Products.Commands
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(
            [FromKeyedServices("catalog")] IDomainEventDispatcher domainEventDispatcher,
            ILogger<UpdateProductCommandHandler> logger)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _logger = logger;
        }

        public async Task HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateProductCommand...");

            // Just for testing the event bus
            var productId = new ProductId(Guid.NewGuid());
            var product = new Product
            {
                Id = productId,
                Name = "Product Name"
            };

            product.ChangeName("New Product Name");

            // Save changes...

            await _domainEventDispatcher.DispatchAndClearAsync(product, cancellationToken);
        }
    }
}
using DepresStore.Modules.Catalog.Core.Entities;
using DepresStore.Shared.Kernel.Cqrs;
using DepresStore.Shared.Kernel.EventBus;
using DepresStore.Shared.Kernel.ValueObjects;
using Microsoft.Extensions.Logging;

namespace DepresStore.Modules.Catalog.Application.Features.Products.Commands
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(
            IEventBus eventBus,
            ILogger<UpdateProductCommandHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateProductCommand...");

            // Just for testing the event bus
            var productId = new ProductId(Guid.NewGuid());
            var product = new Product(productId)
            {
                Name = "Product Name"
            };

            product.ChangeName("New Product Name");

            // Save changes...

            // TODO: Wrap the event bus in a DomainEventDispatcher class?
            foreach (var domainEvent in product.DomainEvents)
            {
                await _eventBus.PublishAsync(domainEvent, cancellationToken);
            }
            product.ClearDomainEvents();
        }
    }
}
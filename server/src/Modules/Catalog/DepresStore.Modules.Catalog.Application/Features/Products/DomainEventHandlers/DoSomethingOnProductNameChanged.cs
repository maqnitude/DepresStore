using DepresStore.Modules.Catalog.Core.DomainEvents;
using DepresStore.Shared.Kernel.EventBus;
using Microsoft.Extensions.Logging;

namespace DepresStore.Modules.Catalog.Application.Features.Products.DomainEventHandlers
{
    public class ProductNameChangedEventHandler : IEventHandler<ProductNameChanged>
    {
        private readonly ILogger<ProductNameChangedEventHandler> _logger;

        public ProductNameChangedEventHandler(ILogger<ProductNameChangedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(ProductNameChanged @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Detected event: Product name changed from \"{OldName}\" to \"{NewName}\" for product {ProductId}",
                @event.OldName, @event.NewName, @event.ProductId.Value);

            _logger.LogInformation("Doing something...");

            return Task.CompletedTask;
        }
    }
}
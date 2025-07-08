using DepresStore.Modules.Catalog.Core.ProductAggregate.Events;
using DepresStore.Shared.Kernel.EventBus;
using Microsoft.Extensions.Logging;

namespace DepresStore.Modules.Catalog.Application.Features.UpdateProduct
{
    public class ProductNameChangedEventHandler : IEventHandler<ProductNameChangedEvent>
    {
        private readonly ILogger<ProductNameChangedEventHandler> _logger;

        public ProductNameChangedEventHandler(ILogger<ProductNameChangedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(ProductNameChangedEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Product name changed from \"{OldName}\" to \"{NewName}\" for product {ProductId}",
                @event.OldName, @event.NewName, @event.ProductId.Value);
            return Task.CompletedTask;
        }
    }
}
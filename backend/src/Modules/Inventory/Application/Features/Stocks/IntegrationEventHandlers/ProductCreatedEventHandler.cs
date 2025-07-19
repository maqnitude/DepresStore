using DepresStore.Modules.Catalog.IntegrationEvents;
using DepresStore.Shared.Kernel.Application;
using Microsoft.Extensions.Logging;

namespace DepresStore.Modules.Inventory.Application.Features.Stocks.IntegrationEventHandlers
{
    public class ProductCreatedEventHandler : IEventHandler<ProductCreated>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;

        public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(ProductCreated @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProductCreated integration event detected");
            return Task.CompletedTask;
        }
    }
}
using DepresStore.Modules.Catalog.Core.Events;
using DepresStore.Shared.Kernel.EventBus;
using Microsoft.Extensions.Logging;

namespace DepresStore.Modules.Catalog.Application.EventHandlers
{
    public class DoSomethingOnProductNameChanged : IEventHandler<ProductNameChanged>
    {
        private readonly ILogger<DoSomethingOnProductNameChanged> _logger;

        public DoSomethingOnProductNameChanged(ILogger<DoSomethingOnProductNameChanged> logger)
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
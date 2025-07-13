using DepresStore.Modules.Catalog.IntegrationEvents;
using DepresStore.Shared.Kernel.Cqrs;
using DepresStore.Shared.Kernel.EventBus;
using Microsoft.Extensions.Logging;

namespace DepresStore.Modules.Catalog.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(
            IEventBus eventBus,
            ILogger<CreateProductCommandHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task HandleAsync(CreateProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateProductCommand...");

            // After commiting transaction
            await _eventBus.PublishAsync(new ProductCreated(), cancellationToken);

            return;
        }
    }
}
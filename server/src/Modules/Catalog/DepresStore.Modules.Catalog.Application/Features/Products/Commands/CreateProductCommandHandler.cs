using DepresStore.Shared.Kernel.Cqrs;
using Microsoft.Extensions.Logging;

namespace DepresStore.Modules.Catalog.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(CreateProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateProductCommand...");
            return Task.CompletedTask;
        }
    }
}
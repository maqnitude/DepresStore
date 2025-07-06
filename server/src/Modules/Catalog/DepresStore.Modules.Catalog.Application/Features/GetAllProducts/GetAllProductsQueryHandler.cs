using DepresStore.Shared.Kernel.Cqrs;
using Microsoft.Extensions.Logging;

namespace DepresStore.Modules.Catalog.Application.Features.GetAllProducts
{
    public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, PaginatedList<ProductDto>>
    {
        private readonly ILogger<GetAllProductsQueryHandler> _logger;

        public GetAllProductsQueryHandler(ILogger<GetAllProductsQueryHandler> logger)
        {
            _logger = logger;
        }

        public Task<PaginatedList<ProductDto>> HandleAsync(
            GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllProductsQuery handled");
            return Task.FromResult(new PaginatedList<ProductDto>());
        }
    }
}
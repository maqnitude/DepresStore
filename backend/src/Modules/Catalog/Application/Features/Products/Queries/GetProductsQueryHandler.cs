using DepresStore.Shared.Kernel.Application;
using DepresStore.Shared.Kernel.Application.Models;
using Microsoft.Extensions.Logging;

namespace DepresStore.Modules.Catalog.Application.Features.Products.Queries
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, PaginatedList<ProductDto>>
    {
        private readonly ILogger<GetProductsQueryHandler> _logger;

        public GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger)
        {
            _logger = logger;
        }

        public Task<PaginatedList<ProductDto>> HandleAsync(
            GetProductsQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetProductsQuery handled");
            return Task.FromResult(new PaginatedList<ProductDto>());
        }
    }
}
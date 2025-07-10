using DepresStore.Shared.Kernel.Cqrs;

namespace DepresStore.Modules.Catalog.Application.Features.Products.Queries
{
    public record GetProductsQuery : IQuery<PaginatedList<ProductDto>> { }
}
using DepresStore.Shared.Kernel.Cqrs;

namespace DepresStore.Modules.Catalog.Application.Features.GetProducts
{
    public record GetProductsQuery : IQuery<PaginatedList<ProductDto>> { }
}
using DepresStore.Shared.Kernel.Cqrs;

namespace DepresStore.Modules.Catalog.Application.Features.GetAllProducts
{
    public record class GetAllProductsQuery : IQuery<PaginatedList<ProductDto>> { }
}
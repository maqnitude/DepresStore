using DepresStore.Shared.Kernel.Application;
using DepresStore.Shared.Kernel.Application.Models;

namespace DepresStore.Modules.Catalog.Application.Features.Products.Queries
{
    public record GetProductsQuery : IQuery<PaginatedList<ProductDto>> { }
}
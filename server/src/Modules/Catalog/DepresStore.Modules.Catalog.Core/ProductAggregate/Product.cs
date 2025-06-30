using DepresStore.SharedKernel;

namespace DepresStore.Modules.Catalog.Core.ProductAggregate
{
    public class Product : Entity<ProductId>
    {
        public string Name { get; set; } = string.Empty;
    }
}
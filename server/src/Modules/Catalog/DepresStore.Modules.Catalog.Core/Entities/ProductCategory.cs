using DepresStore.Modules.Catalog.Core.ValueObjects;
using DepresStore.Shared.Kernel.ValueObjects;

namespace DepresStore.Modules.Catalog.Core.Entities
{
    public class ProductCategory
    {
        public required ProductId ProductId { get; set; }

        public required CategoryId CategoryId { get; set; }

        // Navigations
        public Product Product { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
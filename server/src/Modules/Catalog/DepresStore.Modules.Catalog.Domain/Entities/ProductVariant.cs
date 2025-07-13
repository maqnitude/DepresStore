using DepresStore.Modules.Catalog.Domain.ValueObjects;
using DepresStore.Shared.Kernel;

namespace DepresStore.Modules.Catalog.Domain.Entities
{
    public class ProductVariant : Entity<ProductVariantId>
    {
        public required string Sku { get; set; }

        public List<ProductVariantAttribute> Attributes { get; set; } = [];

        public required ProductId ProductId { get; set; }

        // Navigations
        public Product Product { get; set; } = null!;
    }
}
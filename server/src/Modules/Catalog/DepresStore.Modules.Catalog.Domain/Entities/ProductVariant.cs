using DepresStore.Modules.Catalog.Domain.ValueObjects;
using DepresStore.Shared.Kernel;

namespace DepresStore.Modules.Catalog.Domain.Entities
{
    public class ProductVariant : Entity<ProductVariantId>
    {
        public required ProductId ProductId { get; set; }

        public required string Sku { get; set; }

        public List<ProductVariantAttribute> Attributes { get; set; } = [];

        // Navigations
        public Product Product { get; set; } = null!;
    }
}
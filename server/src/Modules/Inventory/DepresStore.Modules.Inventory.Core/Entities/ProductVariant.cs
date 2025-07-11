using DepresStore.Modules.Inventory.Core.ValueObjects;
using DepresStore.Shared.Kernel;
using DepresStore.Shared.Kernel.ValueObjects;

namespace DepresStore.Modules.Inventory.Core.Entities
{
    public class ProductVariant : AggregateRoot<ProductVariantId>
    {
        public required ProductId ProductId { get; set; }

        public required string Sku { get; set; }

        public int Quantity { get; set; }

        // For EF Core
        private ProductVariant() : base() { }

        public ProductVariant(ProductVariantId productVariantId) : base(productVariantId) { }
    }
}
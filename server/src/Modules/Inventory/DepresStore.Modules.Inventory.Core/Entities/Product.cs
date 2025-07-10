using DepresStore.Shared.Kernel;
using DepresStore.Shared.Kernel.ValueObjects;

namespace DepresStore.Modules.Inventory.Core.Entities
{
    public class Product : AggregateRoot<ProductId>
    {
        public int Quantity { get; set; }

        private Product() : base() { }

        public Product(ProductId productId) : base(productId) { }
    }
}
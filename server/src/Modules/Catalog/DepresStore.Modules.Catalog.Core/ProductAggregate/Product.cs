using DepresStore.Modules.Catalog.Core.Events;
using DepresStore.Shared.Kernel;

namespace DepresStore.Modules.Catalog.Core.ProductAggregate
{
    public class Product : AggregateRoot<ProductId>
    {
        public string Name { get; set; } = string.Empty;

        private Product() : base() { }

        public Product(ProductId productId) : base(productId) { }

        public void ChangeName(string newName)
        {
            if (newName != Name)
            {
                var oldName = Name;
                Name = newName;

                AddDomainEvent(new ProductNameChanged(Id, oldName, newName));
            }
        }
    }
}
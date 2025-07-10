using DepresStore.Modules.Catalog.Core.DomainEvents;
using DepresStore.Shared.Kernel;
using DepresStore.Shared.Kernel.ValueObjects;

namespace DepresStore.Modules.Catalog.Core.Entities
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
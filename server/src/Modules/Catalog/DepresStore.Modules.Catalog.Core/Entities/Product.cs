using DepresStore.Modules.Catalog.Core.DomainEvents;
using DepresStore.Shared.Kernel;
using DepresStore.Shared.Kernel.ValueObjects;

namespace DepresStore.Modules.Catalog.Core.Entities
{
    public class Product : AggregateRoot<ProductId>
    {
        public required string Name { get; set; }

        // Navigations
        public List<Category> Categories { get; } = [];
        public List<ProductCategory> ProductCategories { get; } = [];

        // EF Core lazy-loading proxies require the constructor to be accessible
        // from the inheriting proxy class (use public or protected)
        // Learn more: https://learn.microsoft.com/en-us/ef/core/modeling/constructors
        public Product() : base()
        {
        }

        public Product(ProductId productId) : base(productId)
        {
        }

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
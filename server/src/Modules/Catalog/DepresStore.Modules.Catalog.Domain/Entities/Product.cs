using DepresStore.Modules.Catalog.Domain.Events;
using DepresStore.Modules.Catalog.Domain.ValueObjects;
using DepresStore.Shared.Kernel.Domain;

namespace DepresStore.Modules.Catalog.Domain.Entities
{
    public class Product : AggregateRoot<ProductId>
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        // Navigations
        public List<Category> Categories { get; } = [];
        public List<ProductCategory> ProductCategories { get; } = [];
        public List<ProductVariant> Variants { get; } = [];

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
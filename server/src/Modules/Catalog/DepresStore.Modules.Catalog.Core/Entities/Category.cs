using DepresStore.Modules.Catalog.Core.ValueObjects;
using DepresStore.Shared.Kernel;

namespace DepresStore.Modules.Catalog.Core.Entities
{
    public class Category : AggregateRoot<CategoryId>
    {
        public required CategoryId ParentCategoryId { get; set; }

        public required string Name { get; set; }

        // Navigations
        public List<Product> Products { get; } = [];
        public List<ProductCategory> ProductCategories { get; } = [];

        public Category()
        {
        }

        public Category(CategoryId id) : base(id)
        {
        }
    }
}
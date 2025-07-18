using DepresStore.Modules.Catalog.Domain.ValueObjects;
using DepresStore.Shared.Kernel.Domain;

namespace DepresStore.Modules.Catalog.Domain.Entities
{
    public class Category : AggregateRoot<CategoryId>
    {
        public CategoryId? ParentCategoryId { get; set; }

        public required string Name { get; set; }

        // Navigations
        public Category? ParentCategory { get; set; }
        public List<Category> Subcategories { get; } = [];
        public List<Product> Products { get; } = [];
        public List<ProductCategory> ProductCategories { get; } = [];
    }
}
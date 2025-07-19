using DepresStore.Modules.Catalog.Domain.ValueObjects;

namespace DepresStore.Modules.Catalog.Domain.Entities
{
    /// <summary>
    /// Join entity used to configure many-to-many relationship
    /// between <see cref="Entities.Product"/> and <see cref="Entities.Category"/>.
    /// </summary>
    public class ProductCategory
    {
        public required ProductId ProductId { get; set; }

        public required CategoryId CategoryId { get; set; }

        // Navigations
        public Product Product { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
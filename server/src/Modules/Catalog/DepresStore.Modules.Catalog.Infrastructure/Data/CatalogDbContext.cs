using DepresStore.Modules.Catalog.Domain.Entities;
using DepresStore.Modules.Catalog.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DepresStore.Modules.Catalog.Infrastructure.Data
{
    public class CatalogDbContext : DbContext
    {
        public const string Schema = "DepresStore.Catalog";

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<ProductVariant> Variants { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
            new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
            new ProductCategoryConfiguration().Configure(modelBuilder.Entity<ProductCategory>());
            new ProductVariantConfiguration().Configure(modelBuilder.Entity<ProductVariant>());
        }
    }
}
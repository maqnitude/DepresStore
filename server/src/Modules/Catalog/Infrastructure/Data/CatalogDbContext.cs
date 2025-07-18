using DepresStore.Modules.Catalog.Domain.Entities;
using DepresStore.Modules.Catalog.Domain.ValueObjects;
using DepresStore.Modules.Catalog.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DepresStore.Modules.Catalog.Infrastructure.Data
{
    public class CatalogDbContext : DbContext
    {
        public const string Schema = "Catalog";

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

            // Ignore identity value objects
            // ProductVariantAttribute is an owned type so don't ignore it
            modelBuilder.Ignore<CategoryId>();
            modelBuilder.Ignore<ProductId>();
            modelBuilder.Ignore<ProductVariantId>();

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVariantConfiguration());
        }
    }
}
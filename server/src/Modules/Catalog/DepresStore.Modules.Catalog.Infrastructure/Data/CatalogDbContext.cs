using DepresStore.Modules.Catalog.Core.ProductAggregate;
using DepresStore.Modules.Catalog.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DepresStore.Modules.Catalog.Infrastructure.Data
{
    public class CatalogDbContext : DbContext
    {
        public const string Schema = "DepresStore.Products";

        public DbSet<Product> Products { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
        }
    }
}
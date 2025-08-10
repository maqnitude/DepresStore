using Microsoft.EntityFrameworkCore;

namespace DepresStore.Storefront.Data
{
    public class StorefrontDbContext : DbContext
    {
        public const string Schema = "Storefront";

        public StorefrontDbContext(DbContextOptions<StorefrontDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
        }
    }
}
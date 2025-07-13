using Microsoft.EntityFrameworkCore;

namespace DepresStore.Modules.Inventory.Infrastructure.Data
{
    public class InventoryDbContext : DbContext
    {
        public const string Schema = "DepresStore.Inventory";

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
        }
    }
}
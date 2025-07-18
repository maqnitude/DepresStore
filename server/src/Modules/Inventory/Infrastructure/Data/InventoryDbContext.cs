using DepresStore.Modules.Inventory.Domain.Entities;
using DepresStore.Modules.Inventory.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DepresStore.Modules.Inventory.Infrastructure.Data
{
    public class InventoryDbContext : DbContext
    {
        public const string Schema = "Inventory";

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            new WarehouseConfiguration().Configure(modelBuilder.Entity<Warehouse>());
            new StockConfiguration().Configure(modelBuilder.Entity<Stock>());
        }
    }
}
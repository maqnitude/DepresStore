using DepresStore.Modules.Inventory.Domain.Entities;
using DepresStore.Modules.Inventory.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepresStore.Modules.Inventory.Infrastructure.Data.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasConversion(
                    id => id.Value,
                    value => new StockId(value));

            // Allow same SKU across different warehouse
            builder
                .HasIndex(s => new { s.WarehouseId, s.Sku })
                .IsUnique();
        }
    }
}
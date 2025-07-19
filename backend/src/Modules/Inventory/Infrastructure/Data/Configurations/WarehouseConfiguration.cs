using DepresStore.Modules.Inventory.Domain.Entities;
using DepresStore.Modules.Inventory.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepresStore.Modules.Inventory.Infrastructure.Data.Configurations
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id)
                .HasConversion(
                    id => id.Value,
                    value => new WarehouseId(value));

            // Warehouse <one-to-many> Stock
            builder
                .HasMany(w => w.Stocks)
                .WithOne(s => s.Warehouse)
                .HasForeignKey(s => s.WarehouseId)
                .IsRequired();
        }
    }
}
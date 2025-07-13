using DepresStore.Modules.Catalog.Domain.Entities;
using DepresStore.Modules.Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepresStore.Modules.Catalog.Infrastructure.Data.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.HasKey(pv => pv.Id);
            builder.Property(pv => pv.Id)
                .HasConversion(
                    id => id.Value,
                    value => new ProductVariantId(value));

            builder
                .HasIndex(pv => pv.Sku)
                .IsUnique();

            builder.OwnsMany(
                pv => pv.Attributes, builder =>
                {
                    builder.Property(a => a.Key).IsRequired();
                    builder.Property(a => a.Value).IsRequired();
                    builder.Property(a => a.Type).IsRequired();
                });
        }
    }
}
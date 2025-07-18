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
            builder.ToTable("ProductVariants");

            builder.HasKey(pv => pv.Id);
            builder.Property(pv => pv.Id)
                .HasConversion(
                    id => id.Value,
                    value => new ProductVariantId(value))
                .ValueGeneratedNever();

            builder.Property(pv => pv.ProductId)
                .HasConversion(
                    id => id.Value,
                    value => new ProductId(value));

            builder
                .HasIndex(pv => pv.Sku)
                .IsUnique();

            builder.OwnsMany(
                pv => pv.Attributes, builder =>
                {
                    builder.ToTable("ProductVariantAttributes");

                    builder.Property(a => a.Key)
                        .HasMaxLength(50)
                        .IsRequired();

                    builder.Property(a => a.Value)
                        .HasMaxLength(250)
                        .IsRequired();

                    builder.Property(a => a.Type)
                        .IsRequired();
                });

            // Product <one-to-many> ProductVariant
            builder
                .HasOne(pv => pv.Product)
                .WithMany(p => p.ProductVariants)
                .HasForeignKey(pv => pv.ProductId)
                .IsRequired();
        }
    }
}
using DepresStore.Modules.Catalog.Domain.Entities;
using DepresStore.Modules.Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepresStore.Modules.Catalog.Infrastructure.Data.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");

            builder.HasKey(pc => new { pc.ProductId, pc.CategoryId });

            builder.Property(pc => pc.ProductId)
                .HasConversion(
                    id => id.Value,
                    value => new ProductId(value))
                .ValueGeneratedNever();

            builder.Property(pc => pc.CategoryId)
                .HasConversion(
                    id => id.Value,
                    value => new CategoryId(value))
                .ValueGeneratedNever();

            // Product <one-to-many> ProductCategory
            builder
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId)
                .IsRequired();

            // Category <one-to-many> ProductCategory
            builder
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId)
                .IsRequired();
        }
    }
}
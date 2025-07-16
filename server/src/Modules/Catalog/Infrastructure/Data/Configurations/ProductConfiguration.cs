using DepresStore.Modules.Catalog.Domain.Entities;
using DepresStore.Modules.Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepresStore.Modules.Catalog.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            // Product <many-to-many> Category via ProductCategory join entity
            builder
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity<ProductCategory>();

            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => new ProductId(value));

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
using DepresStore.Modules.Catalog.Core.Entities;
using DepresStore.Modules.Catalog.Core.ValueObjects;
using DepresStore.Shared.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepresStore.Modules.Catalog.Infrastructure.Data.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(pc => pc.ProductId)
                .HasConversion(
                    id => id.Value,
                    value => new ProductId(value));

            builder.Property(pc => pc.CategoryId)
                .HasConversion(
                    id => id.Value,
                    value => new CategoryId(value));
        }
    }
}
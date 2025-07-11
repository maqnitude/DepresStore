using DepresStore.Modules.Catalog.Core.Entities;
using DepresStore.Modules.Catalog.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepresStore.Modules.Catalog.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            // Many-to-many relationship with Product is configured in ProductConfiguration

            builder.Property(c => c.Id)
                .HasConversion(
                    id => id.Value,
                    value => new CategoryId(value));

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
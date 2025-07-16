using DepresStore.Modules.Catalog.Domain.Entities;
using DepresStore.Modules.Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepresStore.Modules.Catalog.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasConversion(
                    id => id.Value,
                    value => new CategoryId(value));

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Many-to-many relationship with Product is configured in ProductConfiguration

            // Category <one-to-many> Category (Subcategory)
            builder
                .HasOne(c => c.ParentCategory)
                .WithMany(pc => pc.Subcategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .IsRequired(false);
        }
    }
}
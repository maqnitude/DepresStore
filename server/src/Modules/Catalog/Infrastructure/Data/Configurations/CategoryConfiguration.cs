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
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasConversion(
                    id => id.Value,
                    value => new CategoryId(value))
                .ValueGeneratedNever();

            builder.Property(c => c.ParentCategoryId)
                .HasConversion(
                    id => id != null ? id.Value : (Guid?)null,
                    value => value.HasValue ? new CategoryId(value.Value) : null);

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Category <one-to-many> Category (Subcategory)
            builder
                .HasOne(c => c.ParentCategory)
                .WithMany(pc => pc.Subcategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .IsRequired(false);
        }
    }
}
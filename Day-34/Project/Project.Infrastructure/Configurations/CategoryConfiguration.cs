using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.Categories;

namespace Project.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(CategoryConstants.CategoryNameMaxLengthValue);
                        
            builder
                .Property(x=>x.CreatedAt)
                .HasDefaultValueSql("getdate()");
            
            builder
                .Property(x=>x.UpdatedAt)
                .HasDefaultValueSql("getdate()");
            
            builder
                .HasMany(s => s.Products)
                .WithOne(g => g.Category)
                .HasForeignKey(s => s.CategoryId);
        }
    }
}

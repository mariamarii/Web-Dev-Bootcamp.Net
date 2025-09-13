using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models;
using Project.Domain.Models.Categories;
using Project.Domain.Models.Products;

namespace Project.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(ProductConstants.ProductNameMaxLengthValue);
            
            builder
                .Property(x=>x.CreatedAt)
                .HasDefaultValueSql("getdate()");
            
            builder
                .Property(x=>x.UpdatedAt)
                .HasDefaultValueSql("getdate()");
            
            builder
                .HasOne<Category>(s => s.Category)
                .WithMany(g => g.Products)
                .HasForeignKey(s => s.CategoryId);
            
            builder
                .HasQueryFilter(p => !p.IsDeleted);
        }
    }
}

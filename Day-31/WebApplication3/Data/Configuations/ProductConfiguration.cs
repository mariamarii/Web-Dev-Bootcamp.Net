using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Configuations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(500);
            
        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
            
        builder.Property(p => p.ImageUrl)
            .IsRequired()
            .HasMaxLength(255);
            
        builder.Property(p => p.CreatedBy)
            .IsRequired();
            
        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<string>();
    }
}
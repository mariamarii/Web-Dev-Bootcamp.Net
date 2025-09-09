using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Configuations;

public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.TemplateName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(e => e.Subject)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(e => e.HtmlContent)
            .IsRequired();
            
        builder.Property(e => e.TextContent)
            .IsRequired();
            
        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
            
        builder.Property(e => e.CreatedAt)
            .IsRequired();
            
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
            
        builder.HasIndex(e => e.TemplateName)
            .IsUnique();
    }
}

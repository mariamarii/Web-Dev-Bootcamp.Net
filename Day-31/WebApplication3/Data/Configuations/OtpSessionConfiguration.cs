using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Configuations;

public class OtpSessionConfiguration : IEntityTypeConfiguration<OtpSession>
{
    public void Configure(EntityTypeBuilder<OtpSession> builder)
    {
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.SessionId)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(o => o.UserId)
            .IsRequired();
            
        builder.Property(o => o.OtpCode)
            .IsRequired()
            .HasMaxLength(10);
            
        builder.Property(o => o.Purpose)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(o => o.CreatedAt)
            .IsRequired();
            
        builder.Property(o => o.ExpiresAt)
            .IsRequired();
            
        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(o => o.SessionId)
            .IsUnique();
            
        builder.HasIndex(o => new { o.UserId, o.Purpose, o.IsUsed });
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.Carts;

namespace Project.Infrastructure.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.UserId)
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()");

        builder
            .Property(x => x.UpdatedAt)
            .HasDefaultValueSql("getdate()");

        builder
            .HasMany(c => c.CartItems)
            .WithOne(ci => ci.Cart)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder
            .HasOne(c => c.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasQueryFilter(c => !c.IsDeleted);
    }
}
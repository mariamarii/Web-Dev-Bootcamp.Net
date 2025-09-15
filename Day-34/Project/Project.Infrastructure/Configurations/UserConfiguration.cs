using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.Users;

namespace Project.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(UserConstants.MaxFirstNameLength);

        builder
            .Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(UserConstants.MaxLastNameLength);

        builder
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(UserConstants.MaxEmailLength);

        builder
            .HasIndex(u => u.Email)
            .IsUnique();

        builder
            .Property(u => u.PhoneNumber)
            .HasMaxLength(UserConstants.MaxPhoneNumberLength);

        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()");

        builder
            .Property(x => x.UpdatedAt)
            .HasDefaultValueSql("getdate()");

        builder
            .HasMany(u => u.Carts)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasQueryFilter(u => !u.IsDeleted);
    }
}
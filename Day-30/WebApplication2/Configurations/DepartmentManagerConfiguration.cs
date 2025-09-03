using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Models;

namespace WebApplication2.Configurations;

public class DepartmentManagerConfiguration : IEntityTypeConfiguration<DepartmentManager>
{
    public void Configure(EntityTypeBuilder<DepartmentManager> builder)
    {
        builder.HasKey(dm => new { dm.EmployeeId, dm.DepartmentId });

        builder.HasOne(dm => dm.Employee)
            .WithMany(e => e.ManagedDepartments)
            .HasForeignKey(dm => dm.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dm => dm.Department)
            .WithMany(d => d.Managers)
            .HasForeignKey(dm => dm.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

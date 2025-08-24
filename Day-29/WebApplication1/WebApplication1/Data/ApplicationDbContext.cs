using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Login> Logins { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.login)
            .WithOne(l => l.employee)
            .HasForeignKey<Login>(l => l.employeeId);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.departmentId);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.role)
            .WithMany(r => r.Employees)
            .HasForeignKey(e => e.RoleId);
    }
}
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : DbContext(opts)
{

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Dependent> Dependents => Set<Dependent>();
    public DbSet<DepartmentManager> DepartmentManagers => Set<DepartmentManager>();
    public DbSet<EmployeeProject> EmployeeProjects => Set<EmployeeProject>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EmployeeProject>()
            .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

        modelBuilder.Entity<EmployeeProject>()
            .HasOne(ep => ep.Employee)
            .WithMany(e => e.EmployeeProjects)
            .HasForeignKey(ep => ep.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EmployeeProject>()
            .HasOne(ep => ep.Project)
            .WithMany(p => p.EmployeeProjects)
            .HasForeignKey(ep => ep.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DepartmentManager>()
            .HasKey(dm => new { dm.EmployeeId, dm.DepartmentId });

        modelBuilder.Entity<DepartmentManager>()
            .HasOne(dm => dm.Employee)
            .WithMany(e => e.ManagedDepartments)
            .HasForeignKey(dm => dm.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DepartmentManager>()
            .HasOne(dm => dm.Department)
            .WithMany(d => d.Managers)
            .HasForeignKey(dm => dm.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Project>()
            .HasOne(p => p.Department)
            .WithMany(d => d.Projects)
            .HasForeignKey(p => p.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Dependent>()
            .HasOne(d => d.Employee)
            .WithMany(e => e.Dependents)
            .HasForeignKey(d => d.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

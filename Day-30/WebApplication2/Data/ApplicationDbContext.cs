using Microsoft.EntityFrameworkCore;
using WebApplication2.Configurations;
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

        // Apply all entity configurations
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new DependentConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentManagerConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeProjectConfiguration());
    }
}

using Microsoft.EntityFrameworkCore;
using WebApplication1.Configurations;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
       
    }
}

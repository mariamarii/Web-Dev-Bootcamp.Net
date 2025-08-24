namespace WebApplication1.Models;

public class Role
{
    public int id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<Employee> Employees { get; set; }
}
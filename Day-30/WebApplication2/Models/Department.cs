
using WebApplication2.Models;
public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Location { get; set; } = String.Empty;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public ICollection<DepartmentManager> Managers { get; set; } = new List<DepartmentManager>();

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
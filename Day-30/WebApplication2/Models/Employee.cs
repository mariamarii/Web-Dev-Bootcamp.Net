namespace WebApplication2.Models;
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public DateTime Dob { get; set; }
    public DateTime Doj { get; set; }
    public string Gender { get; set; } = String.Empty;
    public string? ImagePath { get; set; }

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

    public ICollection<DepartmentManager> ManagedDepartments { get; set; } = new List<DepartmentManager>();
}
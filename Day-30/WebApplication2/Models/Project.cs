namespace WebApplication2.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Location { get; set; } = String.Empty;

    public int DepartmentId { get; set; }
    public virtual Department? Department { get; set; }

    public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
}
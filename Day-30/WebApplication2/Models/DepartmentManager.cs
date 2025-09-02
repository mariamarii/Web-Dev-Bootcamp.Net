namespace WebApplication2.Models;

public class DepartmentManager
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public DateTime Since { get; set; }
}
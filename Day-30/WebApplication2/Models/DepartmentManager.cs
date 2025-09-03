namespace WebApplication2.Models;

public class DepartmentManager
{
    public int EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }

    public int DepartmentId { get; set; }
    public virtual Department? Department { get; set; }

    public DateTime Since { get; set; }
}
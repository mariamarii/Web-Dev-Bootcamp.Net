namespace WebApplication2.Models;

public class Dependent
{
    public int Id { get; set; }
    public string D_Name { get; set; } = String.Empty;
    public string Gender { get; set; } = String.Empty;
    public string Relationship { get; set; } = String.Empty;

    public int EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
}
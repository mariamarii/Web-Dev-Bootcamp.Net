namespace WebApplication2.Dto.Employee;

public class EmployeeWriteDto
{
    public string Name { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public DateTime Dob { get; set; }
    public DateTime Doj { get; set; }
    public string Gender { get; set; } = String.Empty;
    public int DepartmentId { get; set; }
    public List<int> ProjectIds { get; set; } = new();
    public IFormFile? Image { get; set; }
}

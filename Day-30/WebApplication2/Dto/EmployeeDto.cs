
namespace WebApplication2.Dto;
public class EmployeeReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public DateTime Dob { get; set; }
    public DateTime Doj { get; set; }
    public string Gender { get; set; } = String.Empty;
    public string? ImageUrl { get; set; }

    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = String.Empty;
    public List<string> ProjectNames { get; set; } = new();

    public List<ManagedDepartmentDto> ManagedDepartments { get; set; } = new();
}
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
public class ManagedDepartmentDto
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = String.Empty;
    public DateTime Since { get; set; }
}
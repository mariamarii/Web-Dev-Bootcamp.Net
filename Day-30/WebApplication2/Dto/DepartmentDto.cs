
namespace WebApplication2.Dto;
public class DepartmentReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    public List<string> Employees { get; set; } = new();
    public List<ManagerDto> Managers { get; set; } = new();
    public List<string> Projects { get; set; } = new();
}
public class DepartmentWriteDto
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}
public class ManagerDto
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime Since { get; set; }
}
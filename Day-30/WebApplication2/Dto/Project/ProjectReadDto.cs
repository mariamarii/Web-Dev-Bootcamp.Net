namespace WebApplication2.Dto.Project;

public class ProjectReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;

    public List<EmployeeOnProjectDto> Employees { get; set; } = new();
}

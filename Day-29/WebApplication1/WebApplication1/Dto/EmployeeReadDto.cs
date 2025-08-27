namespace WebApplication1.Dto;

public class EmployeeReadDto
{
    public int id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public string DepartmentName { get; set; }
    public string RoleName { get; set; }
    public string LoginUsername { get; set; }
}
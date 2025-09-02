namespace WebApplication2.Dto;
public class DependentReadDto
{
    public int Id { get; set; }
    public string D_Name { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
}
public class DependentWriteDto
{
    public string D_Name { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
}
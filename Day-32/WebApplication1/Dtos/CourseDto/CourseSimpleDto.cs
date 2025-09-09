namespace WebApplication1.Dtos.CourseDto;

public class CourseSimpleDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Hours { get; set; }
}

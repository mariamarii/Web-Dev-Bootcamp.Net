namespace WebApplication1.Dtos.CourseDto;

public class UpdateCourseRequestDto
{
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Hours { get; set; }
}
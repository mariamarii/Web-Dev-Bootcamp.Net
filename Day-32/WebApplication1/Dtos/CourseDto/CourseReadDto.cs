using WebApplication1.Models;

namespace WebApplication1.Dtos.CourseDto;

public class CourseReadDto
{
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Hours { get; set; }

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
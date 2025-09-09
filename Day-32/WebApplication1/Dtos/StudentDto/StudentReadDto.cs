using WebApplication1.Dtos.CourseDto;

namespace WebApplication1.Dtos.StudentDto;

public class StudentReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public ICollection<CourseSimpleDto> Courses { get; set; } = new List<CourseSimpleDto>();
}
using WebApplication1.Dtos.StudentDto;

namespace WebApplication1.Dtos.CourseDto;

public class CourseReadDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Hours { get; set; }

    public ICollection<StudentSimpleDto> Students { get; set; } = new List<StudentSimpleDto>();
}
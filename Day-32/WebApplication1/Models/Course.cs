namespace WebApplication1.Models;

public class Course
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Hours { get; set; }

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
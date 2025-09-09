namespace WebApplication1.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
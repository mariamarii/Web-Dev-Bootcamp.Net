using MediatR;
using WebApplication1.Global;
using WebApplication1.Models;

namespace WebApplication1.Features.Students.Command.Models;

public class CreateStudentDto : IRequest<Response>
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
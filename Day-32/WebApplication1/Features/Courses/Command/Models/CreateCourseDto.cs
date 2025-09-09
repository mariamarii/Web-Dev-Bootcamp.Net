using MediatR;
using WebApplication1.Global;

namespace WebApplication1.Features.Courses.Command.Models;

public class CreateCourseDto : IRequest<Response>
{
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Hours { get; set; }
}

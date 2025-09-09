using MediatR;
using WebApplication1.Global;

namespace WebApplication1.Features.Students.Command.Models;

public class CreateStudentSimpleDto : IRequest<Response>
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}

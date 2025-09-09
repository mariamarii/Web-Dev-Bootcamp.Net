using MediatR;
using WebApplication1.Global;

namespace WebApplication1.Features.Students.Command.RemoveCourseFromStudent;

public class RemoveCourseFromStudentDto : IRequest<Response>
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
}

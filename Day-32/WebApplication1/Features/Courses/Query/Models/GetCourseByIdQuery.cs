using MediatR;
using WebApplication1.Global;

namespace WebApplication1.Features.Courses.Query.Models;

public class GetCourseByIdQuery : IRequest<Response>
{
    public int Id { get; set; }

    public GetCourseByIdQuery(int id)
    {
        Id = id;
    }
}

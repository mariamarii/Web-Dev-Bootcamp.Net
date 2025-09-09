using MediatR;
using WebApplication1.Global;

namespace WebApplication1.Features.Students.Query.Models;

public class GetStudentByIdQuery : IRequest<Response>
{
    public int Id { get; set; }

    public GetStudentByIdQuery(int id)
    {
        Id = id;
    }
}

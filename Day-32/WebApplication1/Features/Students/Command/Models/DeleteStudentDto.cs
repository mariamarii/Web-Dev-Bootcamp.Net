using MediatR;
using WebApplication1.Global;

namespace WebApplication1.Features.Students.Command.Models;

public class DeleteStudentDto : IRequest<Response>
{
    public int Id { get; set; }
}
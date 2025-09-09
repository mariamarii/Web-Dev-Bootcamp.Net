using MediatR;
using WebApplication1.Features.Students.Command.Models;
using WebApplication1.Global;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Specifications.Student;
using System.Net;

namespace WebApplication1.Features.Students.Command.Handler;

public class DeleteStudentHandler(IGenericRepository<Student> studentRepository) : IRequestHandler<DeleteStudentDto, Response>
{

    public async Task<Response> Handle(DeleteStudentDto request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new StudentByIdWithCoursesSpec(request.Id);
            var existingStudent = await studentRepository.FirstOrDefaultAsync(spec, cancellationToken);
            
            if (existingStudent == null)
            {
                return new Response(data: null, "Student not found", HttpStatusCode.NotFound);
            }

            await studentRepository.DeleteAsync(existingStudent, cancellationToken);
            return new Response(data: null, "Student deleted successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new Response(data: null, $"Error deleting student: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}

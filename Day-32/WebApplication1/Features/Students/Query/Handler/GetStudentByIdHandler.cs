using AutoMapper;
using MediatR;
using WebApplication1.Features.Students.Query.Models;
using WebApplication1.Global;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Specifications.Student;
using WebApplication1.Dtos.StudentDto;
using System.Net;

namespace WebApplication1.Features.Students.Query.Handler;

public class GetStudentByIdHandler(IGenericRepository<Student> studentRepository, IMapper mapper) : IRequestHandler<GetStudentByIdQuery, Response>
{

    public async Task<Response> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new StudentByIdWithCoursesSpec(request.Id);
            var student = await studentRepository.FirstOrDefaultAsync(spec, cancellationToken);
            
            if (student == null)
            {
                return new Response(null, "Student not found", HttpStatusCode.NotFound);
            }

            var studentDto = mapper.Map<StudentReadDto>(student);
            return new Response(studentDto, "Student retrieved successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new Response(null, $"Error retrieving student: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}

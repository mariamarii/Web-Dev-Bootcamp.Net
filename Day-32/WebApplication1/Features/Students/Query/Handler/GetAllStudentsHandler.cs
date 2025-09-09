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

public class GetAllStudentsHandler(IGenericRepository<Student> studentRepository, IMapper mapper) : IRequestHandler<GetAllStudentsQuery, Response>
{

    public async Task<Response> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new StudentsWithCoursesSpec();
            var students = await studentRepository.ListAsync(spec, cancellationToken);
            
            var studentDtos = mapper.Map<IReadOnlyList<StudentReadDto>>(students);
            return new Response(studentDtos, "Students retrieved successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new Response(null, $"Error retrieving students: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}

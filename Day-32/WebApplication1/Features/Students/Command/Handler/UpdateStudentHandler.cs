using AutoMapper;
using MediatR;
using WebApplication1.Features.Students.Command.Models;
using WebApplication1.Global;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Specifications.Student;
using WebApplication1.Dtos.StudentDto;
using System.Net;

namespace WebApplication1.Features.Students.Command.Handler;

public class UpdateStudentHandler(IGenericRepository<Student> studentRepository, IMapper mapper) : IRequestHandler<UpdateStudentDto, Response>
{

    public async Task<Response> Handle(UpdateStudentDto request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new StudentByIdWithCoursesSpec(request.Id);
            var existingStudent = await studentRepository.FirstOrDefaultAsync(spec, cancellationToken);
            
            if (existingStudent == null)
            {
                return new Response(data: null, "Student not found", HttpStatusCode.NotFound);
            }

            mapper.Map(request, existingStudent);
            await studentRepository.UpdateAsync(existingStudent, cancellationToken);

            var studentDto = mapper.Map<StudentReadDto>(existingStudent);
            return new Response(studentDto, "Student updated successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new Response(data: null, $"Error updating student: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}

using AutoMapper;
using MediatR;
using WebApplication1.Features.Students.Command.Models;
using WebApplication1.Global;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Dtos.StudentDto;
using System.Net;

namespace WebApplication1.Features.Students.Command.Handler;

public class CreateStudentSimpleHandler(IGenericRepository<Student> studentRepository, IMapper mapper) : IRequestHandler<CreateStudentSimpleDto, Response>
{
    public async Task<Response> Handle(CreateStudentSimpleDto request, CancellationToken cancellationToken)
    {
        try
        {
            var student = mapper.Map<Student>(request);
            await studentRepository.AddAsync(student, cancellationToken);

            var studentDto = mapper.Map<StudentReadDto>(student);
            return new Response(studentDto, "Student created successfully", HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return new Response(data: null, $"Error creating student: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}

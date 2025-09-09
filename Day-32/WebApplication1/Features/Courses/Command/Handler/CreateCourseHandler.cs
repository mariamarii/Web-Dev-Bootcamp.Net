using AutoMapper;
using MediatR;
using WebApplication1.Features.Courses.Command.Models;
using WebApplication1.Global;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Dtos.CourseDto;
using System.Net;

namespace WebApplication1.Features.Courses.Command.Handler;

public class CreateCourseHandler(IGenericRepository<Course> courseRepository, IMapper mapper) : IRequestHandler<CreateCourseDto, Response>
{

    public async Task<Response> Handle(CreateCourseDto request, CancellationToken cancellationToken)
    {
        try
        {
            var course = mapper.Map<Course>(request);
            await courseRepository.AddAsync(course, cancellationToken);

            var courseDto = mapper.Map<CourseReadDto>(course);
            return new Response(courseDto, "Course created successfully", HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return new Response(null, $"Error creating course: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}

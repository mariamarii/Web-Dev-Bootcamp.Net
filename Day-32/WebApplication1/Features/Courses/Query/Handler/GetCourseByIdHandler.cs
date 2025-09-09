using AutoMapper;
using MediatR;
using WebApplication1.Features.Courses.Query.Models;
using WebApplication1.Global;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Specifications.Course;
using WebApplication1.Dtos.CourseDto;
using System.Net;

namespace WebApplication1.Features.Courses.Query.Handler;

public class GetCourseByIdHandler(IGenericRepository<Course> courseRepository, IMapper mapper) : IRequestHandler<GetCourseByIdQuery, Response>
{

    public async Task<Response> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new CourseByIdWithStudentsSpec(request.Id);
            var course = await courseRepository.FirstOrDefaultAsync(spec, cancellationToken);
            
            if (course == null)
            {
                return new Response(null, "Course not found", HttpStatusCode.NotFound);
            }

            var courseDto = mapper.Map<CourseReadDto>(course);
            return new Response(courseDto, "Course retrieved successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new Response(null, $"Error retrieving course: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}

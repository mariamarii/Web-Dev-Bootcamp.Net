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

public class GetAllCoursesHandler(IGenericRepository<Course> courseRepository, IMapper mapper) : IRequestHandler<GetAllCoursesQuery, Response>
{

    public async Task<Response> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new CoursesWithStudentsSpec();
            var courses = await courseRepository.ListAsync(spec, cancellationToken);
            
            var courseDtos = mapper.Map<IReadOnlyList<CourseReadDto>>(courses);
            return new Response(courseDtos, "Courses retrieved successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new Response(null, $"Error retrieving courses: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}

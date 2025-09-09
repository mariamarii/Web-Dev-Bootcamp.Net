using AutoMapper;
using MediatR;
using WebApplication1.Features.Courses.Command.Models;
using WebApplication1.Global;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Specifications.Course;
using WebApplication1.Dtos.CourseDto;
using System.Net;

namespace WebApplication1.Features.Courses.Command.Handler;

public class UpdateCourseHandler(IGenericRepository<Course> courseRepository, IMapper mapper) : IRequestHandler<UpdateCourseDto, Response>
{

    public async Task<Response> Handle(UpdateCourseDto request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new CourseByIdWithStudentsSpec(request.Id);
            var existingCourse = await courseRepository.FirstOrDefaultAsync(spec, cancellationToken);
            
            if (existingCourse == null)
            {
                return new Response(null, "Course not found", HttpStatusCode.NotFound);
            }

            mapper.Map(request, existingCourse);
            await courseRepository.UpdateAsync(existingCourse, cancellationToken);

            var courseDto = mapper.Map<CourseReadDto>(existingCourse);
            return new Response(courseDto, "Course updated successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new Response(null, $"Error updating course: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}

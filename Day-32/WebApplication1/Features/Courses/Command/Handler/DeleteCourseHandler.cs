using MediatR;
using WebApplication1.Features.Courses.Command.Models;
using WebApplication1.Global;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Specifications.Course;
using System.Net;

namespace WebApplication1.Features.Courses.Command.Handler;

public class DeleteCourseHandler(IGenericRepository<Course> courseRepository) : IRequestHandler<DeleteCourseDto, Response>
{

    public async Task<Response> Handle(DeleteCourseDto request, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new CourseByIdWithStudentsSpec(request.Id);
            var existingCourse = await courseRepository.FirstOrDefaultAsync(spec, cancellationToken);
            
            if (existingCourse == null)
            {
                return new Response(null, "Course not found", HttpStatusCode.NotFound);
            }

            await courseRepository.DeleteAsync(existingCourse, cancellationToken);
            return new Response(null, "Course deleted successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new Response(null, $"Error deleting course: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}

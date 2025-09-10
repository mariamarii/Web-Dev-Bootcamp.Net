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
            var spec = new CoursesWithFiltersSpec(
                code: request.Code,
                title: request.Title,
                minHours: request.MinHours,
                maxHours: request.MaxHours,
                studentName: request.StudentName,
                minStudentAge: request.MinStudentAge,
                maxStudentAge: request.MaxStudentAge,
                hasStudents: request.HasStudents,
                sortBy: request.SortBy,
                isDescending: request.IsDescending,
                page: request.Page,
                pageSize: request.PageSize
            );
            
            var courses = await courseRepository.ListAsync(spec, cancellationToken);
            
            var countSpec = new CoursesWithFiltersSpec(
                code: request.Code,
                title: request.Title,
                minHours: request.MinHours,
                maxHours: request.MaxHours,
                studentName: request.StudentName,
                minStudentAge: request.MinStudentAge,
                maxStudentAge: request.MaxStudentAge,
                hasStudents: request.HasStudents
            );
            var totalCount = await courseRepository.CountAsync(countSpec, cancellationToken);
            
            var courseDtos = mapper.Map<IReadOnlyList<CourseReadDto>>(courses);
            
            var paginationInfo = new
            {
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
            
            var result = new
            {
                Courses = courseDtos,
                Pagination = paginationInfo
            };
            
            return new Response(result, "Courses retrieved successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new Response(null, $"Error retrieving courses: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}

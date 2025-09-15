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
            var filterDto = new CourseFilterDto
            {
                Code = request.Code,
                Title = request.Title,
                MinHours = request.MinHours,
                MaxHours = request.MaxHours,
                StudentName = request.StudentName,
                MinStudentAge = request.MinStudentAge,
                MaxStudentAge = request.MaxStudentAge,
                HasStudents = request.HasStudents,
                SortBy = request.SortBy,
                IsDescending = request.IsDescending,
                Page = request.Page,
                PageSize = request.PageSize
            };

            var spec = new CoursesWithFiltersSpec(filterDto);
            var courses = await courseRepository.ListAsync(spec, cancellationToken);

            var countFilterDto = new CourseFilterDto
            {
                Code = request.Code,
                Title = request.Title,
                MinHours = request.MinHours,
                MaxHours = request.MaxHours,
                StudentName = request.StudentName,
                MinStudentAge = request.MinStudentAge,
                MaxStudentAge = request.MaxStudentAge,
                HasStudents = request.HasStudents
            };
            var countSpec = new CoursesWithFiltersSpec(countFilterDto);
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

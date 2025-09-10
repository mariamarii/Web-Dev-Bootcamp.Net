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
            var spec = new StudentsWithFiltersSpec(
                name: request.Name,
                minAge: request.MinAge,
                maxAge: request.MaxAge,
                courseCode: request.CourseCode,
                courseTitle: request.CourseTitle,
                hasCourses: request.HasCourses,
                sortBy: request.SortBy,
                isDescending: request.IsDescending,
                page: request.Page,
                pageSize: request.PageSize
            );
            
            var students = await studentRepository.ListAsync(spec, cancellationToken);
            
            var countSpec = new StudentsWithFiltersSpec(
                name: request.Name,
                minAge: request.MinAge,
                maxAge: request.MaxAge,
                courseCode: request.CourseCode,
                courseTitle: request.CourseTitle,
                hasCourses: request.HasCourses
            );
            var totalCount = await studentRepository.CountAsync(countSpec, cancellationToken);
            
            var studentDtos = mapper.Map<IReadOnlyList<StudentReadDto>>(students);
            
            var paginationInfo = new
            {
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
            
            var result = new
            {
                Students = studentDtos,
                Pagination = paginationInfo
            };
            
            return new Response(result, "Students retrieved successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return new Response(null, $"Error retrieving students: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}

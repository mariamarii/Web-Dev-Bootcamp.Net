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
            var filterDto = new StudentFilterDto
            {
                Name = request.Name,
                MinAge = request.MinAge,
                MaxAge = request.MaxAge,
                CourseCode = request.CourseCode,
                CourseTitle = request.CourseTitle,
                HasCourses = request.HasCourses,
                SortBy = request.SortBy,
                IsDescending = request.IsDescending,
                Page = request.Page,
                PageSize = request.PageSize
            };
            
            var spec = new StudentsWithFiltersSpec(filterDto);
            var students = await studentRepository.ListAsync(spec, cancellationToken);
            
            var countFilterDto = new StudentFilterDto
            {
                Name = request.Name,
                MinAge = request.MinAge,
                MaxAge = request.MaxAge,
                CourseCode = request.CourseCode,
                CourseTitle = request.CourseTitle,
                HasCourses = request.HasCourses
            };
            var countSpec = new StudentsWithFiltersSpec(countFilterDto);
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

using MediatR;
using WebApplication1.Global;

namespace WebApplication1.Features.Students.Query.Models;

public class GetAllStudentsQuery : IRequest<Response>
{
    public string? Name { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    public string? CourseCode { get; set; }
    public string? CourseTitle { get; set; }
    public bool? HasCourses { get; set; }
    public string? SortBy { get; set; } = "Name"; 
    public bool IsDescending { get; set; } = false;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

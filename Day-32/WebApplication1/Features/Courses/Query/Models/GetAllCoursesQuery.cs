using MediatR;
using WebApplication1.Global;

namespace WebApplication1.Features.Courses.Query.Models;

public class GetAllCoursesQuery : IRequest<Response>
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public int? MinHours { get; set; }
    public int? MaxHours { get; set; }
    public string? StudentName { get; set; }
    public int? MinStudentAge { get; set; }
    public int? MaxStudentAge { get; set; }
    public bool? HasStudents { get; set; }
    public string? SortBy { get; set; } = "Title"; 
    public bool IsDescending { get; set; } = false;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

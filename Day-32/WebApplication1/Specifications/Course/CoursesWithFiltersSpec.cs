using WebApplication1.Models;
using WebApplication1.Specifications.Base;

namespace WebApplication1.Specifications.Course;

public class CoursesWithFiltersSpec : BaseSpecification<Models.Course>
{
    public CoursesWithFiltersSpec(
        string? code = null,
        string? title = null,
        int? minHours = null,
        int? maxHours = null,
        string? studentName = null,
        int? minStudentAge = null,
        int? maxStudentAge = null,
        bool? hasStudents = null,
        string? sortBy = "Title",
        bool isDescending = false,
        int? page = null,
        int? pageSize = null)
    {
        AddInclude(c => c.Students);

        AddCriteria(c => 
            (string.IsNullOrWhiteSpace(code) || c.Code.ToLower().Contains(code.ToLower())) &&
            (string.IsNullOrWhiteSpace(title) || c.Title.ToLower().Contains(title.ToLower())) &&
            (!minHours.HasValue || c.Hours >= minHours.Value) &&
            (!maxHours.HasValue || c.Hours <= maxHours.Value) &&
            (string.IsNullOrWhiteSpace(studentName) || c.Students.Any(s => s.Name.ToLower().Contains(studentName.ToLower()))) &&
            (!minStudentAge.HasValue || c.Students.Any(s => s.Age >= minStudentAge.Value)) &&
            (!maxStudentAge.HasValue || c.Students.Any(s => s.Age <= maxStudentAge.Value)) &&
            (!hasStudents.HasValue || (hasStudents.Value ? c.Students.Any() : !c.Students.Any()))
        );

        switch (sortBy?.ToLower())
        {
            case "code":
                if (isDescending)
                    AddOrderByDescending(c => c.Code);
                else
                    AddOrderBy(c => c.Code);
                break;
            case "hours":
                if (isDescending)
                    AddOrderByDescending(c => c.Hours);
                else
                    AddOrderBy(c => c.Hours);
                break;
            case "id":
                if (isDescending)
                    AddOrderByDescending(c => c.Id);
                else
                    AddOrderBy(c => c.Id);
                break;
            case "title":
            default:
                if (isDescending)
                    AddOrderByDescending(c => c.Title);
                else
                    AddOrderBy(c => c.Title);
                break;
        }

        if (page.HasValue && pageSize.HasValue && page.Value > 0 && pageSize.Value > 0)
        {
            ApplyPaging((page.Value - 1) * pageSize.Value, pageSize.Value);
        }
    }
}

using WebApplication1.Models;
using WebApplication1.Specifications.Base;

namespace WebApplication1.Specifications.Student;

public class StudentsWithFiltersSpec : BaseSpecification<Models.Student>
{
    public StudentsWithFiltersSpec(
        string? name = null,
        int? minAge = null,
        int? maxAge = null,
        string? courseCode = null,
        string? courseTitle = null,
        bool? hasCourses = null,
        string? sortBy = "Name",
        bool isDescending = false,
        int? page = null,
        int? pageSize = null)
    {
        AddInclude(s => s.Courses);

        AddCriteria(s => 
            (string.IsNullOrWhiteSpace(name) || s.Name.ToLower().Contains(name.ToLower())) &&
            (!minAge.HasValue || s.Age >= minAge.Value) &&
            (!maxAge.HasValue || s.Age <= maxAge.Value) &&
            (string.IsNullOrWhiteSpace(courseCode) || s.Courses.Any(c => c.Code.ToLower().Contains(courseCode.ToLower()))) &&
            (string.IsNullOrWhiteSpace(courseTitle) || s.Courses.Any(c => c.Title.ToLower().Contains(courseTitle.ToLower()))) &&
            (!hasCourses.HasValue || (hasCourses.Value ? s.Courses.Any() : !s.Courses.Any()))
        );

        switch (sortBy?.ToLower())
        {
            case "age":
                if (isDescending)
                    AddOrderByDescending(s => s.Age);
                else
                    AddOrderBy(s => s.Age);
                break;
            case "id":
                if (isDescending)
                    AddOrderByDescending(s => s.Id);
                else
                    AddOrderBy(s => s.Id);
                break;
            case "name":
            default:
                if (isDescending)
                    AddOrderByDescending(s => s.Name);
                else
                    AddOrderBy(s => s.Name);
                break;
        }

        if (page.HasValue && pageSize.HasValue && page.Value > 0 && pageSize.Value > 0)
        {
            ApplyPaging((page.Value - 1) * pageSize.Value, pageSize.Value);
        }
    }
}

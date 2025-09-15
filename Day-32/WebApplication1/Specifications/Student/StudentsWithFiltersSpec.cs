using WebApplication1.Models;
using WebApplication1.Specifications.Base;
using WebApplication1.Dtos.StudentDto;

namespace WebApplication1.Specifications.Student;

public class StudentsWithFiltersSpec : BaseSpecification<Models.Student>
{
    public StudentsWithFiltersSpec(StudentFilterDto filter)
    {
        AddInclude(s => s.Courses);

        SetCriteria(s =>
            (string.IsNullOrWhiteSpace(filter.Name) || s.Name.ToLower().Contains(filter.Name.ToLower())) &&
            (!filter.MinAge.HasValue || s.Age >= filter.MinAge.Value) &&
            (!filter.MaxAge.HasValue || s.Age <= filter.MaxAge.Value) &&
            (string.IsNullOrWhiteSpace(filter.CourseCode) || s.Courses.Any(c => c.Code.ToLower().Contains(filter.CourseCode.ToLower()))) &&
            (string.IsNullOrWhiteSpace(filter.CourseTitle) || s.Courses.Any(c => c.Title.ToLower().Contains(filter.CourseTitle.ToLower()))) &&
            (!filter.HasCourses.HasValue || (filter.HasCourses.Value ? s.Courses.Any() : !s.Courses.Any()))
        );

        switch (filter.SortBy?.ToLower())
        {
            case "age":
                if (filter.IsDescending)
                    SetOrderByDescending(s => s.Age);
                else
                    SetOrderBy(s => s.Age);
                break;
            case "id":
                if (filter.IsDescending)
                    SetOrderByDescending(s => s.Id);
                else
                    SetOrderBy(s => s.Id);
                break;
            case "name":
            default:
                if (filter.IsDescending)
                    SetOrderByDescending(s => s.Name);
                else
                    SetOrderBy(s => s.Name);
                break;
        }

        if (filter.Page > 0 && filter.PageSize > 0)
        {
            EnablePaging((filter.Page - 1) * filter.PageSize, filter.PageSize);
        }
    }
}

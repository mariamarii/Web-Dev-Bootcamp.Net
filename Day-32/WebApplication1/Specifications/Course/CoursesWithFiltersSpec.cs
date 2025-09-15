using WebApplication1.Models;
using WebApplication1.Specifications.Base;
using WebApplication1.Dtos.CourseDto;

namespace WebApplication1.Specifications.Course;

public class CoursesWithFiltersSpec : BaseSpecification<Models.Course>
{
    public CoursesWithFiltersSpec(CourseFilterDto filter)
    {
        AddInclude(c => c.Students);

        SetCriteria(c =>
            (string.IsNullOrWhiteSpace(filter.Code) || c.Code.ToLower().Contains(filter.Code.ToLower())) &&
            (string.IsNullOrWhiteSpace(filter.Title) || c.Title.ToLower().Contains(filter.Title.ToLower())) &&
            (!filter.MinHours.HasValue || c.Hours >= filter.MinHours.Value) &&
            (!filter.MaxHours.HasValue || c.Hours <= filter.MaxHours.Value) &&
            (string.IsNullOrWhiteSpace(filter.StudentName) || c.Students.Any(s => s.Name.ToLower().Contains(filter.StudentName.ToLower()))) &&
            (!filter.MinStudentAge.HasValue || c.Students.Any(s => s.Age >= filter.MinStudentAge.Value)) &&
            (!filter.MaxStudentAge.HasValue || c.Students.Any(s => s.Age <= filter.MaxStudentAge.Value)) &&
            (!filter.HasStudents.HasValue || (filter.HasStudents.Value ? c.Students.Any() : !c.Students.Any()))
        );

        switch (filter.SortBy?.ToLower())
        {
            case "code":
                if (filter.IsDescending)
                    SetOrderByDescending(c => c.Code);
                else
                    SetOrderBy(c => c.Code);
                break;
            case "hours":
                if (filter.IsDescending)
                    SetOrderByDescending(c => c.Hours);
                else
                    SetOrderBy(c => c.Hours);
                break;
            case "id":
                if (filter.IsDescending)
                    SetOrderByDescending(c => c.Id);
                else
                    SetOrderBy(c => c.Id);
                break;
            case "title":
            default:
                if (filter.IsDescending)
                    SetOrderByDescending(c => c.Title);
                else
                    SetOrderBy(c => c.Title);
                break;
        }

        if (filter.Page > 0 && filter.PageSize > 0)
        {
            EnablePaging((filter.Page - 1) * filter.PageSize, filter.PageSize);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Specifications.Base;

namespace WebApplication1.Specifications.Course;

public class CoursesByStudentNameSpec : BaseSpecification<Models.Course>
{
    public CoursesByStudentNameSpec(string studentName)
    {
        AddInclude(c => c.Students);

        SetCriteria(c => c.Students.Any(s => EF.Functions.Like(s.Name.ToLower(), $"%{studentName.ToLower()}%")));
    }
}

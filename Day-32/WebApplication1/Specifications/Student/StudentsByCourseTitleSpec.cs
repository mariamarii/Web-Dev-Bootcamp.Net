using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Specifications.Base;

namespace WebApplication1.Specifications.Student;

public class StudentsByCourseTitleSpec : BaseSpecification<Models.Student>
{
    public StudentsByCourseTitleSpec(string courseTitle)
    {
        AddInclude(s => s.Courses);
        
        AddCriteria(s => s.Courses.Any(c => EF.Functions.Like(c.Title.ToLower(), $"%{courseTitle.ToLower()}%")));
    }
}

using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Specifications.Base;

namespace WebApplication1.Specifications.Student;

public class StudentsByCourseCodeSpec : BaseSpecification<Models.Student>
{
    public StudentsByCourseCodeSpec(string courseCode)
    {
        AddInclude(s => s.Courses);
        
        AddCriteria(s => s.Courses.Any(c => EF.Functions.Like(c.Code.ToLower(), $"%{courseCode.ToLower()}%")));
    }
}

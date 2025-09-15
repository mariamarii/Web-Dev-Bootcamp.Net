using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Specifications.Base;

namespace WebApplication1.Specifications.Course;

public class CoursesByStudentAgeRangeSpec : BaseSpecification<Models.Course>
{
    public CoursesByStudentAgeRangeSpec(int? minAge = null, int? maxAge = null)
    {
        AddInclude(c => c.Students);
        
        if (minAge.HasValue && maxAge.HasValue)
        {
            SetCriteria(c => c.Students.Any(s => s.Age >= minAge.Value && s.Age <= maxAge.Value));
        }
        else if (minAge.HasValue)
        {
            SetCriteria(c => c.Students.Any(s => s.Age >= minAge.Value));
        }
        else if (maxAge.HasValue)
        {
            SetCriteria(c => c.Students.Any(s => s.Age <= maxAge.Value));
        }
    }
}

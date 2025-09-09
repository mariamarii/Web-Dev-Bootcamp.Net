using WebApplication1.Specifications.Base;

namespace WebApplication1.Specifications.Student;

public sealed class StudentByIdWithCoursesSpec : BaseSpecification<Models.Student>
{
    public StudentByIdWithCoursesSpec(int id) : base(s => s.Id == id)
    {
        AddInclude(s => s.Courses);
    }
}
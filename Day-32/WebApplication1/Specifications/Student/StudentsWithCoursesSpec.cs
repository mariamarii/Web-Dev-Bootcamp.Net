using WebApplication1.Specifications.Base;

namespace WebApplication1.Specifications.Student;

public sealed class StudentsWithCoursesSpec : BaseSpecification<Models.Student>
{
        public StudentsWithCoursesSpec()
        {
            AddInclude(s => s.Courses);
            ApplyOrderBy(s => s.Name);
        }

        public StudentsWithCoursesSpec(int skip, int take) : this() => ApplyPaging(skip, take);
}

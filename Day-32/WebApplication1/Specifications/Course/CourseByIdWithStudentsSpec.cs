using WebApplication1.Specifications.Base;

namespace WebApplication1.Specifications.Course;

public sealed class CourseByIdWithStudentsSpec : BaseSpecification<Models.Course>
{
        public CourseByIdWithStudentsSpec(int id) : base(c => c.Id == id)
        {
            AddInclude(c => c.Students);
        }
}

using WebApplication1.Specifications.Base;

namespace WebApplication1.Specifications.Course;

public sealed class CoursesWithStudentsSpec : BaseSpecification<Models.Course>
{
    public CoursesWithStudentsSpec()
    {
        AddInclude(c => c.Students);
        ApplyOrderBy(c => c.Title);
    }

    public CoursesWithStudentsSpec(int skip, int take) : this() => ApplyPaging(skip, take);
}
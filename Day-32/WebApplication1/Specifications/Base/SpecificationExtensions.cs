using WebApplication1.Specifications.Base;

namespace WebApplication1.Specifications.Base;

public static class SpecificationExtensions
{
    public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right)
    {
        return new AndSpecification<T>(left, right);
    }
}

public class AndSpecification<T> : BaseSpecification<T>
{
    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        if (left.Criteria != null)
            AddCriteria(left.Criteria);
            
        if (right.Criteria != null)
            AddCriteria(right.Criteria);

        foreach (var include in left.Includes)
            AddInclude(include);
            
        foreach (var include in right.Includes)
            AddInclude(include);

        if (left.OrderBy != null)
            AddOrderBy(left.OrderBy);
        else if (right.OrderBy != null)
            AddOrderBy(right.OrderBy);

        if (left.OrderByDescending != null)
            AddOrderByDescending(left.OrderByDescending);
        else if (right.OrderByDescending != null)
            AddOrderByDescending(right.OrderByDescending);
    }
}

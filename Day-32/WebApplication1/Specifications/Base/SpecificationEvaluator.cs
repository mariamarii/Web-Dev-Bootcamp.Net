using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Specifications.Base;

public static class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> ApplySpecification(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        ArgumentNullException.ThrowIfNull(inputQuery);
        ArgumentNullException.ThrowIfNull(specification);

        var query = inputQuery;

        query = ApplyFiltering(query, specification);
        query = ApplyIncludes(query, specification);
        query = ApplyOrdering(query, specification);
        query = ApplyPaging(query, specification);

        return query;
    }

    private static IQueryable<T> ApplyFiltering(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        return query;
    }

    private static IQueryable<T> ApplyIncludes(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.Includes?.Any() == true)
        {
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return query;
    }

    private static IQueryable<T> ApplyOrdering(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        return query;
    }

    private static IQueryable<T> ApplyPaging(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.IsPagingEnabled &&
            specification.Skip.HasValue &&
            specification.Take.HasValue)
        {
            query = query.Skip(specification.Skip.Value).Take(specification.Take.Value);
        }

        return query;
    }
}
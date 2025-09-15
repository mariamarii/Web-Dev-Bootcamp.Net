using System.Linq.Expressions;

namespace WebApplication1.Specifications.Base;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    protected BaseSpecification() { }

    protected BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
    }

    public Expression<Func<T, bool>>? Criteria { get; private set; }
    public List<Expression<Func<T, object>>> Includes { get; } = [];
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public int? Take { get; private set; }
    public int? Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        ArgumentNullException.ThrowIfNull(includeExpression);
        Includes.Add(includeExpression);
    }

    protected void SetCriteria(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
    }

    protected void SetOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        ArgumentNullException.ThrowIfNull(orderByExpression);
        OrderBy = orderByExpression;
        OrderByDescending = null;
    }

    protected void SetOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        ArgumentNullException.ThrowIfNull(orderByDescExpression);
        OrderByDescending = orderByDescExpression;
        OrderBy = null;
    }

    protected void EnablePaging(int skip, int take)
    {
        if (skip < 0) throw new ArgumentOutOfRangeException(nameof(skip), "Skip cannot be negative");
        if (take <= 0) throw new ArgumentOutOfRangeException(nameof(take), "Take must be greater than zero");

        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    protected void DisablePaging()
    {
        Skip = null;
        Take = null;
        IsPagingEnabled = false;
    }
}
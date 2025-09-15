using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Specifications.Base;

namespace WebApplication1.Repositories;

public class GenericRepository<T>(ApplicationDbContext ctx) : IGenericRepository<T> where T : class
{
    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default) 
        => await ctx.Set<T>().FindAsync(new object[] { id }, ct);

    public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken ct = default) 
        => await ctx.Set<T>().ToListAsync(ct);

    public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken ct = default) 
        => await ApplySpec(spec).FirstOrDefaultAsync(ct);

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken ct = default) 
        => await ApplySpec(spec).ToListAsync(ct);

    public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken ct = default) 
        => await ApplySpec(spec).CountAsync(ct);

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await ctx.Set<T>().AddAsync(entity, ct);
        await ctx.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        ctx.Set<T>().Update(entity);
        await ctx.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        ctx.Set<T>().Remove(entity);
        await ctx.SaveChangesAsync(ct);
    }

    private IQueryable<T> ApplySpec(ISpecification<T> spec) 
        => SpecificationEvaluator<T>.ApplySpecification(ctx.Set<T>().AsQueryable(), spec);
}
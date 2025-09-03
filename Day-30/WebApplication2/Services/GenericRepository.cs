using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;   
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Services;

public class GenericRepository<TEntity>(ApplicationDbContext context) 
    : IGenericRepository<TEntity> where TEntity : class
{
    public void Add(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
    }

    public void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    public TEntity? GetById(int id)
    {
        return context.Set<TEntity>().Find(id);
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public IEnumerable<TEntity> GetAll(int pageNumber, int pageSize)
    {
        return context.Set<TEntity>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await context.Set<TEntity>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
using Ardalis.Specification.EntityFrameworkCore;
using Project.Application.Abstractions.Repositories;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repositories;

public class Repository<TEntity>(ApplicationDbContext dbContext)
    : RepositoryBase<TEntity>(dbContext), IRepository<TEntity> where TEntity : class;

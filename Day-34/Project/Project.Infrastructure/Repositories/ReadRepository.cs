using Ardalis.Specification.EntityFrameworkCore;
using Project.Application.Abstractions.Repositories;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repositories;

public class ReadRepository<TEntity>(ApplicationDbContext dbContext)
    : RepositoryBase<TEntity>(dbContext), IReadRepository<TEntity> where TEntity : class;

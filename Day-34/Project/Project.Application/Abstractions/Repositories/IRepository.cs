using Ardalis.Specification;

namespace Project.Application.Abstractions.Repositories;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class;

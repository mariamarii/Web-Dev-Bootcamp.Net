using Ardalis.Specification;

namespace Project.Application.Abstractions.Repositories;

public interface IReadRepository<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class;

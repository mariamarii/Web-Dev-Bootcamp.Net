namespace WebApplication2.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public void Add(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public TEntity? GetById(int id);
        public Task<TEntity?> GetByIdAsync(int id);
        public IEnumerable<TEntity> GetAll(int pageNumber, int pageSize);
        public Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber, int pageSize);
        public Task SaveChangesAsync();
    }
}
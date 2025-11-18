namespace TourApp.Application.Contracts.Repositories;

public interface IGenericRepository<TEntity> : IUnitOfWork where TEntity: class
{
    public Task<TEntity?> GetAsync(Specification<TEntity>? specification, CancellationToken cancellationToken);
    public Task<IList<TEntity>> GetManyAsync(
        Specification<TEntity>? specification, CancellationToken cancellationToken);
    
    public Task<int> CountAsync(Specification<TEntity>? specification, CancellationToken cancellationToken);
    
    public Task AddAsync(TEntity entity);

    public void Update(TEntity entity);

    public void Update(TEntity entity, params string[] props);

    public void Delete(TEntity entity);
}
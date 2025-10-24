namespace TourApp.Application.Contracts.Repositories.Base;

public interface IReadRepository<TEntity, TKey> where TEntity : class
{
    public Task<TEntity?> GetAsync(Specification<TEntity>? specification);
    public Task<IList<TEntity>> GetManyAsync(Specification<TEntity>? specification);
    
    public Task<int> CountAsync(Specification<TEntity>? specification);
}
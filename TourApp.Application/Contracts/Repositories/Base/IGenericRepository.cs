namespace TourApp.Application.Contracts.Repositories.Base;

public interface IGenericRepository<TEntity, TKey> : IReadRepository<TEntity, TKey>, IWriteRepository<TEntity, TKey>
    where TEntity: class
{
    
}
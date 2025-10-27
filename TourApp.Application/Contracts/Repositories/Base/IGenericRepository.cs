namespace TourApp.Application.Contracts.Repositories.Base;

public interface IGenericRepository<TEntity> : IReadRepository<TEntity>, IWriteRepository<TEntity>
    where TEntity: class
{
    
}
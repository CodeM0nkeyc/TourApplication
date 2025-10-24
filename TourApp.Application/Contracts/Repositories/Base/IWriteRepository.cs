namespace TourApp.Application.Contracts.Repositories.Base;

public interface IWriteRepository<TEntity, TKey> where TEntity : class
{
    public Task AddAsync(TEntity entity);

    public void Update(TEntity entity);

    public void Update(TEntity entity,
        params string[] propNames);

    public void Delete(TEntity entity);

    public Task SaveAsync();
}
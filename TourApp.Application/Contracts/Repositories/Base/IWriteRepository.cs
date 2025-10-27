namespace TourApp.Application.Contracts.Repositories.Base;

public interface IWriteRepository<TEntity> where TEntity : class
{
    public Task AddAsync(TEntity entity);

    public void Update(TEntity entity);

    public void Update(TEntity entity,
        params string[] props);

    public void Delete(TEntity entity);

    public Task SaveAsync();
}
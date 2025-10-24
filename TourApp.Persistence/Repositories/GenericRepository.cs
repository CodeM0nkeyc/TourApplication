using TourApp.Application.Contracts.Repositories.Base;

namespace TourApp.Persistence.Repositories;

public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
{
    protected readonly ApplicationDbContext dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public virtual async Task<TEntity?> GetAsync(Specification<TEntity>? specification)
    {
        var result = await dbContext.Set<TEntity>()
            .ApplySpecification(specification)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return result;
    }

    public virtual async Task<IList<TEntity>> GetManyAsync(Specification<TEntity>? specification)
    {
        var result = await dbContext.Set<TEntity>()
            .ApplySpecification(specification)
            .AsNoTracking()
            .ToListAsync();

        return result;
    }

    public async Task<int> CountAsync(Specification<TEntity>? specification)
    {
        return await dbContext.Set<TEntity>()
            .ApplySpecification(specification)
            .CountAsync();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await dbContext.AddAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        dbContext.Update(entity);
    }

    public virtual void Update(TEntity entity, params string[] propNames)
    {
        dbContext.Attach(entity);
        
        foreach (string prop in propNames)
        {
            dbContext.Entry(entity)
                .Property(prop).IsModified = true;
        }

        dbContext.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        dbContext.Remove(entity);
    }

    public virtual async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
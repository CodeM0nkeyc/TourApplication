namespace TourApp.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
        dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public virtual async Task<TEntity?> GetAsync(Specification<TEntity>? specification)
    {
        var result = await dbContext.Set<TEntity>()
            .ApplySpecification(specification)
            .FirstOrDefaultAsync();

        return result;
    }

    public virtual async Task<IList<TEntity>> GetManyAsync(Specification<TEntity>? specification)
    {
        var result = await dbContext.Set<TEntity>()
            .ApplySpecification(specification)
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
        if (dbContext.Entry(entity).State == EntityState.Detached)
        {
            dbContext.Update(entity);
        }
    }

    public virtual void Update(TEntity entity, params string[] props)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
        {
            dbContext.Attach(entity);
        }
        
        foreach (var prop in props)
        {
            dbContext.Entry(entity)
                .Property(prop).IsModified = true;
        }
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
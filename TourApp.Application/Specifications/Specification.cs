namespace TourApp.Application.Specifications;

public class Specification<TEntity> where TEntity : class
{
    private readonly List<Expression<Func<TEntity, object>>> _includes = new();
    
    protected readonly Filter<TEntity> Filter = new Filter<TEntity>();
    
    public Expression<Func<TEntity, bool>> Criteria => Filter.FilterExpression;
    
    public IReadOnlyCollection<Expression<Func<TEntity, object>>> Includes => _includes;
    public (Expression<Func<TEntity, object>> OrderBy, bool Descending)? OrderBy { get; protected set; }

    public int? PageSize { get; protected set; }

    public int PageIndex { get; protected set; }

    public Specification()
    {
    }

    public Specification(Expression<Func<TEntity, bool>> criteria)
    {
        Filter.And(criteria);
    }

    protected Specification<TEntity> AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        _includes.Add(includeExpression);
        return this;
    }
    
    protected virtual Specification<TEntity> MergeWith(Specification<TEntity> spec)
    {
        Filter.And(spec.Filter.FilterExpression);
        _includes.AddRange(spec._includes);
        
        OrderBy = spec.OrderBy;
        PageSize = spec.PageSize;
        PageIndex = spec.PageIndex;
        
        return this;
    }

    public Specification<TEntity> And(Specification<TEntity> spec)
    {
        return MergeWith(spec);
    }
}
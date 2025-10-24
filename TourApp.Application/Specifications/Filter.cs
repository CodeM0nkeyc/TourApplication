namespace TourApp.Application.Specifications;

public class Filter<TEntity>
{
    private Expression _filterCriteria;
    private ParameterExpression _parameter;
    
    public Expression<Func<TEntity, bool>> FilterExpression 
        => Expression.Lambda<Func<TEntity, bool>>(_filterCriteria, _parameter);

    public Filter() : this(x => true)
    {
    }
    
    public Filter(Expression<Func<TEntity, bool>> filterExpression)
    {
        _filterCriteria = filterExpression.Body;
        _parameter = filterExpression.Parameters[0];
    }
    
    public Filter<TEntity> And(Expression<Func<TEntity, bool>> criteria)
    {
        var changedExpr = new ParameterReplacer(_parameter).Visit(criteria.Body);
        _filterCriteria = Expression.AndAlso(_filterCriteria, changedExpr);
        return this;
    }
    
    public Filter<TEntity> Or(Expression<Func<TEntity, bool>> criteria)
    {
        var changedExpr = new ParameterReplacer(_parameter).Visit(criteria.Body);
        _filterCriteria = Expression.OrElse(_filterCriteria, changedExpr);
        return this;
    }
    
    public Filter<TEntity> Or(IEnumerable<Expression<Func<TEntity, bool>>> criteria)
    {
        Expression body = Expression.Constant(false);

        foreach (var criteriaExpr in criteria)
        {
            var changedExpr = new ParameterReplacer(_parameter).Visit(criteriaExpr.Body);
            body = Expression.OrElse(body, changedExpr);
        }
        
        _filterCriteria = Expression.AndAlso(_filterCriteria, body);

        return this;
    }
    
    public Filter<TEntity> Not(Expression<Func<TEntity, bool>> criteria)
    {
        var changedExpr = new ParameterReplacer(_parameter).Visit(criteria.Body);
        _filterCriteria = Expression.AndAlso(_filterCriteria, Expression.Not(changedExpr));
        return this;
    }
}
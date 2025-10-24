namespace TourApp.Persistence.Extensions;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> ApplySpecification<TEntity>(
        this IQueryable<TEntity> query, Specification<TEntity>? specification)
    where TEntity : class
    {
        if (specification is not null)
        {
            query = query.Where(specification.Criteria);

            foreach (Expression<Func<TEntity, object>> include in specification.Includes)
            {
                query = query.Include(include);
            }

            if (specification.OrderBy.HasValue)
            {
                var (expression, descending) = specification.OrderBy.Value;

                query = descending
                    ? query.OrderByDescending(expression) 
                    : query.OrderBy(expression);
            }

            if (specification.PageSize.HasValue)
            {
                query = query.Skip(specification.PageSize.Value * specification.PageIndex);
                query = query.Take(specification.PageSize.Value);
            }
        }
        
        return query;
    }
}
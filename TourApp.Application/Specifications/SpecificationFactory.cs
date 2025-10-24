namespace TourApp.Application.Specifications;

public abstract class SpecificationFactory<TEntity, TSpecSettings> where TEntity : class
{
    public abstract Specification<TEntity>? CreateSpecification(TSpecSettings settings);
}
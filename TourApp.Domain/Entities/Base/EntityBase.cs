namespace TourApp.Domain.Entities.Base;

public abstract class EntityBase<TKey>
{
    public TKey Id { get; set; }
}
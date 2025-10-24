namespace TourApp.Application.Features.Tours.Specifications;

public class TourOrderBySpecification : Specification<Tour>
{
    public TourOrderBySpecification(TourOrderSettings orderBy)
    {
        OrderBy = (orderBy.ToExpression(), orderBy.Descending);
    }
}
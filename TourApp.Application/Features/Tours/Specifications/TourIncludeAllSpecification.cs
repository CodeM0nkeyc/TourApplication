namespace TourApp.Application.Features.Tours.Specifications;

public class TourIncludeAllSpecification : Specification<Tour>
{
    public TourIncludeAllSpecification()
    {
        AddInclude(tour => tour.TourPricingDetails);
    }
}
namespace TourApp.Application.Features.Tours.Specifications;

public class TourRemainingPlacesSpecification : Specification<Tour>
{
    public TourRemainingPlacesSpecification(int remainingPlaces) 
        : base(tour => tour.RemainingPlaces >= remainingPlaces)
    {
    }
}
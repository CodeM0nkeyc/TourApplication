namespace TourApp.Application.Features.Tours.Specifications;

public class TourNameSpecification : Specification<Tour>
{
    public TourNameSpecification(string heading) 
        : base(tour => tour.Heading == heading)
    {
    }
}
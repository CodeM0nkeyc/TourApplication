namespace TourApp.Application.Features.Tours.Specifications;

public class TourIdSpecification : Specification<Tour>
{
    public TourIdSpecification(int id) 
        : base(tour => tour.Id == id)
    {
    }
}
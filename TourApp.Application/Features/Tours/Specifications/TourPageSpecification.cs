namespace TourApp.Application.Features.Tours.Specifications;

public class TourPageSpecification : Specification<Tour>
{
    public TourPageSpecification(int? pageIndex)
    {
        PageSize = 24;
        PageIndex = pageIndex ?? 0;
    }
}
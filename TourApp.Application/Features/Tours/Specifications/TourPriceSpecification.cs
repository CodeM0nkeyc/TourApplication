namespace TourApp.Application.Features.Tours.Specifications;

public class TourPriceSpecification : Specification<Tour>
{
    public TourPriceSpecification(decimal? lowerBound, decimal? upperBound, bool withDiscount) 
    {
        if (lowerBound.HasValue)
            Filter.And(tour => tour.Price >= lowerBound.Value);
        if (upperBound.HasValue)
            Filter.And(tour => tour.Price <= upperBound.Value);
        if (withDiscount)
            Filter.And(tour => tour.DiscountPercent != null);
    }
}
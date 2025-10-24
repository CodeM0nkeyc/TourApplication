namespace TourApp.Application.Features.Tours.Specifications.Common;

public record TourOrderSettings(
    OrderByTourProperty Property,
    bool Descending
)
{
    public Expression<Func<Tour, object>> ToExpression()
    {
        switch (Property)
        {
            case OrderByTourProperty.Price:
                return tour => tour.Price;
            case OrderByTourProperty.Rating:
                return tour => tour.Rating;
            case OrderByTourProperty.Difficulty:
                return tour => tour.Difficulty;
            case OrderByTourProperty.StartDate:
                return tour => tour.StartDate;
            default:
                throw new ArgumentException("Not supported property for sorting:  " + 
                                            Enum.GetName(typeof(OrderByTourProperty), Property));
        }
    }
}
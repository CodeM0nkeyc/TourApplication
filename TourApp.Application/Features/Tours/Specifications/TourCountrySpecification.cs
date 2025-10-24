namespace TourApp.Application.Features.Tours.Specifications;

public class TourCountrySpecification : Specification<Tour>
{
    public TourCountrySpecification(IEnumerable<string> countries)
    {
        Filter.Or(countries.Select<string, Expression<Func<Tour, bool>>>(
            country => (tour => tour.Country == country)));
    }
}
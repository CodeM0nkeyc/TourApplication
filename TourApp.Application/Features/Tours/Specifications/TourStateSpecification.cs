using TourApp.Domain.Entities.Tour.Common;

namespace TourApp.Application.Features.Tours.Specifications;

public class TourStateSpecification : Specification<Tour>
{
    public TourStateSpecification(IEnumerable<TourState> tourStates)
    {
        Filter.Or(tourStates.Select<TourState, Expression<Func<Tour, bool>>>(
            tourState => (tour => tour.State == tourState)));
    }
}
using TourApp.Domain.Entities.Tour.Common;

namespace TourApp.Application.Features.Tours.Specifications;

public class TourDifficultySpecification : Specification<Tour>
{
    public TourDifficultySpecification(IEnumerable<TourDifficulty> difficulties)
    {
        Filter.Or(difficulties.Select<TourDifficulty, Expression<Func<Tour, bool>>>(
            difficulty => (tour => tour.Difficulty == difficulty)));
    }
}
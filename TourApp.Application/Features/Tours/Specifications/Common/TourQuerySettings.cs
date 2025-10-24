using TourApp.Domain.Entities.Tour.Common;

namespace TourApp.Application.Features.Tours.Specifications.Common;

public record TourQuerySettings
(
    int? Id = null,
    string? Heading = null,
    TourPriceFilter? PriceSettings = null,
    HashSet<TourDifficulty>? Difficulties = null,
    HashSet<TourState>? States = null,
    HashSet<string>? Countries = null,
    TourOrderSettings? OrderSettings = null,
    int? RemainingPlaces = null,
    int? PageIndex = null
);
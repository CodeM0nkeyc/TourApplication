namespace TourApp.Application.Features.Tours.Specifications.Common;

public record TourPriceFilter
(
    decimal? LowerBound,
    decimal? UpperBound,
    bool WithDiscount
);
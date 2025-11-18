namespace TourApp.Application.Features.Tours.Queries.GetTourCountries;

public record GetTourCountriesQuery() : IRequest<Result<IList<string>>>;
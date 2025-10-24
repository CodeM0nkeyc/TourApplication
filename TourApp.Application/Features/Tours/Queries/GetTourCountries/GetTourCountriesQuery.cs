namespace TourApp.Application.Features.Tours.Queries.GetTourCountries;

public record GetTourCountriesQuery() : IRequest<IList<string>>;
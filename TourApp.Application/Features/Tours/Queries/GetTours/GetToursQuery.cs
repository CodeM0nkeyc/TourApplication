namespace TourApp.Application.Features.Tours.Queries.GetTours;

public record GetToursQuery(TourQuerySettings? Settings) : IRequest<IList<TourDetailsDto>>;
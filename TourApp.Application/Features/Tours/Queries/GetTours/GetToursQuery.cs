namespace TourApp.Application.Features.Tours.Queries.GetTours;

public record GetToursQuery(TourQuerySettings? Settings) : IRequest<Result<IList<TourDetailsDto>>>;
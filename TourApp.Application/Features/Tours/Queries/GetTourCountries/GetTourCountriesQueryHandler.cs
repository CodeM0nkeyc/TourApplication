namespace TourApp.Application.Features.Tours.Queries.GetTourCountries;

public class GetTourCountriesQueryHandler : IRequestHandler<GetTourCountriesQuery, Result<IList<string>>>
{
    private readonly ITourRepository _tourRepository;

    public GetTourCountriesQueryHandler(ITourRepository tourRepository)
    {
        _tourRepository = tourRepository;
    }
    
    public async Task<Result<IList<string>>> Handle(GetTourCountriesQuery request, CancellationToken cancellationToken)
    {
        IList<string> countries = await _tourRepository.GetTourCountriesAsync(cancellationToken);
        return Result<IList<string>>.Success(countries)!;
    }
}
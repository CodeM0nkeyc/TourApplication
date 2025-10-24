using TourApp.Application.Features.Tours.Contracts;
using TourApp.Application.Features.Tours.Contracts.Repositories;

namespace TourApp.Application.Features.Tours.Queries.GetTourCountries;

public class GetTourCountriesQueryHandler : IRequestHandler<GetTourCountriesQuery, IList<string>>
{
    private readonly ITourRepository _tourRepository;

    public GetTourCountriesQueryHandler(ITourRepository tourRepository)
    {
        _tourRepository = tourRepository;
    }
    
    public async Task<IList<string>> Handle(GetTourCountriesQuery request, CancellationToken cancellationToken)
    {
        IList<string> countries = await _tourRepository.GetTourCountriesAsync();
        return countries;
    }
}
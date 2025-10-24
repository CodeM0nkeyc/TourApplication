using TourApp.Application.Features.Tours.Contracts;
using TourApp.Application.Features.Tours.Contracts.Repositories;

namespace TourApp.Application.Features.Tours.Queries.GetTours;

public class GetToursQueryHandler : IRequestHandler<GetToursQuery, IList<TourDetailsDto>>
{
    private readonly ITourRepository _tourRepository;
    private readonly IMapper _mapper;
    private readonly TourSpecificationFactory _specificationFactory;

    public GetToursQueryHandler(
        ITourRepository tourRepository, 
        IMapper mapper, 
        TourSpecificationFactory specificationFactory)
    {
        _tourRepository = tourRepository;
        _mapper = mapper;
        _specificationFactory = specificationFactory;
    }
    
    public async Task<IList<TourDetailsDto>> Handle(GetToursQuery request, CancellationToken cancellationToken)
    {
        Specification<Tour>? spec = _specificationFactory.CreateSpecification(request.Settings);
        IList<Tour> tours = await _tourRepository.GetManyAsync(spec);
        return _mapper.Map<IList<TourDetailsDto>>(tours);
    }
}
namespace TourApp.Application.Features.Tours.Queries.GetTours;

public class GetToursQueryHandler : IRequestHandler<GetToursQuery, Result<IList<TourDetailsDto>>>
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
    
    public async Task<Result<IList<TourDetailsDto>>> Handle(GetToursQuery request, CancellationToken cancellationToken)
    {
        Specification<Tour>? spec = _specificationFactory.CreateSpecification(request.Settings);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        IList<Tour> tours = await _tourRepository.GetManyAsync(spec, cancellationToken);
        IList<TourDetailsDto> tourDtos = _mapper.Map<IList<TourDetailsDto>>(tours);

        return Result<IList<TourDetailsDto>>.Success(tourDtos)!;
    }
}
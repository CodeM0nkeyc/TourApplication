namespace TourApp.Web.Api.Controllers.Tours;

[Route("api/[controller]")]
[ApiController]
public class ToursController : ControllerBase
{
    private readonly IMediator _mediator;

    public ToursController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTours(
        [FromQuery] TourQuerySettings settings, CancellationToken cancellationToken)
    {
        GetToursQuery query = new GetToursQuery(settings);
        Result<IList<TourDetailsDto>> data = await _mediator.Send(query, cancellationToken);
        return Ok(data);
    }

    [HttpGet("countries")]
    public async Task<IActionResult> GetTourCountries(CancellationToken cancellationToken)
    {
        Result<IList<string>> countries = await _mediator.Send(new GetTourCountriesQuery(), cancellationToken);
        return Ok(countries);
    }
}
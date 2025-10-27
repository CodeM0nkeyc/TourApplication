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
    public async Task<ActionResult<IList<TourDetailsDto>>> GetTours(
        [FromQuery] TourQuerySettings settings)
    {
        Console.WriteLine(settings.ToString());
        GetToursQuery query = new GetToursQuery(settings);
        IList<TourDetailsDto> data = await _mediator.Send(query);
        return Ok(data);
    }

    [HttpGet("countries")]
    public async Task<ActionResult<IList<string>>> GetTourCountries()
    {
        IList<string> countries = await _mediator.Send(new GetTourCountriesQuery());
        return Ok(countries);
    }
}
namespace TourApp.Web.Api.Controllers.Account;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private Task AssignAuthenticationCookie(AuthenticationResponse response)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, response.UserId!.Value.ToString()),
            new Claim(ClaimTypes.Role, response.UserRole!.Value.ToString())
        };

        ClaimsIdentity identity = 
            new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

        AuthenticationProperties cookieProps = new AuthenticationProperties()
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(2)
        };

        return HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, principal, cookieProps);
    }
    
    [HttpGet("exists")]
    public async Task<IActionResult> CheckIfExists([FromQuery] string email)
    {
        bool userUnique = await _mediator.Send(new UserExistsQuery(email));

        return Ok(userUnique);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LogIn(AuthenticationRequest request)
    {
        if (HttpContext.User.Identity?.IsAuthenticated == true)
        {
            return Ok(Result.Success());
        }
            
        Result<AuthenticationResponse?> authenticationResult = 
            await _mediator.Send(new AuthenticateUserCommand(request));

        if (authenticationResult.IsSuccess)
        {
            await AssignAuthenticationCookie(authenticationResult.Data!);

            return Ok(authenticationResult);
        }

        return BadRequest(authenticationResult);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        Result registrationResult = await _mediator.Send(new RegisterUserCommand(request));

        if (registrationResult.IsSuccess)
        {
            return Ok(registrationResult);
        }
        
        return BadRequest(registrationResult);
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmRegistration(CodeConfirmation confirmation)
    {
        Result confirmationResult = await _mediator.Send(new ConfirmUserRegisterCommand(confirmation));

        if (confirmationResult.IsSuccess)
        {
            return Ok(confirmationResult);
        }
        
        return BadRequest(confirmationResult);
    }
}
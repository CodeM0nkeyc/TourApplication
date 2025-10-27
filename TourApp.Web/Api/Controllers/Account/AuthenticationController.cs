namespace TourApp.Web.Api.Controllers.Account
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<ActionResult<AuthenticationResult>> Authenticate(AuthenticationRequest request)
        {
            if (HttpContext.User.Identity?.IsAuthenticated == true)
            {
                return Ok(nameof(AuthenticationResult.Success));
            }
            
            AuthenticationResponse response = await _mediator.Send(new AuthenticateUserCommand(request));

            if (response.AuthenticationResult == AuthenticationResult.Success)
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

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, principal, cookieProps);
                return LocalRedirect("/home");
            }

            return BadRequest(response.AuthenticationResult);
        }
    }
}

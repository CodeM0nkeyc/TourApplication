using TourApp.Application.Contracts.Services.Account;

namespace TourApp.Application.Features.Users.Commands.AuthenticateUser;

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticationResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticateUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    public async Task<AuthenticationResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.AuthenticateWithPasswordAsync(request.AuthenticationRequest);
    }
}
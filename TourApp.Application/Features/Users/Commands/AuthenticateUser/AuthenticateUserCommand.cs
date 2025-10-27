namespace TourApp.Application.Features.Users.Commands.AuthenticateUser;

public record AuthenticateUserCommand(
    AuthenticationRequest AuthenticationRequest
) : IRequest<AuthenticationResponse>;
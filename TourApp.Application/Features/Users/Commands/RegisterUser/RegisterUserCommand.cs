namespace TourApp.Application.Features.Users.Commands.RegisterUser;

public record RegisterUserCommand(
    RegistrationRequest RegistrationRequest
) : IRequest<Result>;
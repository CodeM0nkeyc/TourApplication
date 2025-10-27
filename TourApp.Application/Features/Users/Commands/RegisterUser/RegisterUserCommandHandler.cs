using TourApp.Application.Contracts.Services.Account;

namespace TourApp.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegistrationResult>
{
    private readonly IRegistrationService _registrationService;

    public RegisterUserCommandHandler(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }
    
    public async Task<RegistrationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await _registrationService.RegisterAsync(request.RegistrationRequest);
    }
}
using TourApp.Application.Contracts.Services.Account;

namespace TourApp.Application.Features.Users.Commands.ConfirmUserRegister;

public class ConfirmUserRegisterCommandHandler : IRequestHandler<ConfirmUserRegisterCommand, ConfirmationResult>
{
    private readonly IRegistrationService _registrationService;

    public ConfirmUserRegisterCommandHandler(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }
    
    public async Task<ConfirmationResult> Handle(
        ConfirmUserRegisterCommand request, CancellationToken cancellationToken)
    {
        return await _registrationService.ConfirmRegistrationAsync(request.Email, request.ConfirmationCode);
    }
}
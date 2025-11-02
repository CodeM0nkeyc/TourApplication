namespace TourApp.Application.Features.Users.Commands.ConfirmUserRegister;

public class ConfirmUserRegisterCommandHandler : IRequestHandler<ConfirmUserRegisterCommand, Result>
{
    private readonly IRegistrationService _registrationService;

    public ConfirmUserRegisterCommandHandler(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }
    
    public async Task<Result> Handle(
        ConfirmUserRegisterCommand request, CancellationToken cancellationToken)
    {
        return await _registrationService.ConfirmRegistrationAsync(
            request.Confirmation.Email, request.Confirmation.Code);
    }
}
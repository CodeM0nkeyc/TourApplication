namespace TourApp.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
{
    private readonly IRegistrationService _registrationService;
    private readonly IValidator<RegistrationRequest> _validator;

    public RegisterUserCommandHandler(
        IRegistrationService registrationService, IValidator<RegistrationRequest> validator)
    {
        _registrationService = registrationService;
        _validator = validator;
    }
    
    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = _validator.Validate(request.RegistrationRequest);

        if (validationResult.IsValid)
        {
            return await _registrationService.RegisterAsync(request.RegistrationRequest);
        }
        
        Result result = Result.Failure(validationResult.Errors.ToErrors());
        
        return result;
    }
}
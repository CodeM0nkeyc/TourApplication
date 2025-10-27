namespace TourApp.Infrastructure.Services.Account;

public class RegistrationService : IRegistrationService
{
    private const string _subject = "Email confirmation";
    
    private readonly SenderSettings _senderSettings;
    
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    
    private readonly IEmailService _emailService;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IConfirmationGenerator _confirmationGenerator;

    public RegistrationService(
        IUserService userService, IUserRepository userRepository, 
        IEmailService emailService, IPasswordHashService passwordHashService,
        IConfirmationGenerator confirmationGenerator, IOptions<SenderSettings> senderSettings)
    {
        _userService = userService;
        _userRepository = userRepository;
        _emailService = emailService;
        _passwordHashService = passwordHashService;
        _confirmationGenerator = confirmationGenerator;
        _senderSettings = senderSettings.Value;
    }

    private string GetConfirmationEmailBody(int confirmationCode)
    {
        return $"Your confirmation code is {confirmationCode}";
    }
    
    public async Task<RegistrationResult> RegisterAsync(RegistrationRequest request)
    {
        bool isUnique = await _userService.CheckUniquenessAsync(request.Email, request.PhoneNumber);
        
        if (!isUnique)
        {
            return RegistrationResult.AlreadyRegistered;
        }

        string recepientName = $"{request.LastName} {request.FirstName}";
        int confirmationCode = _confirmationGenerator.GenerateCode();
        string emailBody = GetConfirmationEmailBody(confirmationCode);
        
        EmailMessage confirmationEmail = new EmailMessage()
        {
            FromAddress = _senderSettings.FromAddress,
            FromName = _senderSettings.FromName,
            Subject = _subject,
            ToAddress = request.Email,
            ToName = recepientName,
            Body = emailBody
        };

        try
        {
            await _emailService.SendEmailAsync(confirmationEmail);
        }
        catch
        {
            return RegistrationResult.ConfirmationEmailError;
        }

        AppUser newUser = request.CreateAppUser(confirmationCode, _passwordHashService);
        
        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveAsync();
        
        return RegistrationResult.ConfirmationSent;
    }

    public async Task<ConfirmationResult> ConfirmRegistrationAsync(string email, int confirmationCode)
    {
        AppUserIdentity identity = await _userRepository.GetUserIdentityByEmailAsync(email) 
                                   ?? throw new NoConfirmationEmailException(email);
        
        ConfirmationCode? storedCode = identity.ConfirmationCode;

        if (storedCode is null)
        {
            return identity.EmailConfirmed 
                ? ConfirmationResult.AlreadyConfirmed 
                : throw new InvalidOperationException("No confirmation code");
        }

        if (storedCode.ExpireAt.CompareTo(DateTime.UtcNow) < 0)
        {
            return ConfirmationResult.Expired;
        }

        if (storedCode.Code != confirmationCode)
        {
            return ConfirmationResult.NoMatch;
        }

        AppUser confirmedUser = new AppUser()
        {
            Id = identity.Id,
            Identity = identity
        };

        identity.EmailConfirmed = true;
        identity.ConfirmationCode = null;
        
        _userRepository.Update(confirmedUser);

        return ConfirmationResult.Success;
    }
}
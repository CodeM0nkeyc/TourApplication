namespace TourApp.Infrastructure.Services.Account;

public class RegistrationService : IRegistrationService
{
    private const string _subject = "Email confirmation";
    
    private readonly SenderSettings _senderSettings;
    
    private readonly IUserRepository _userRepository;
    private readonly IUserIdentityRepository _userIdentityRepository;

    private readonly IEmailService _emailService;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IConfirmationGenerator _confirmationGenerator;
    private readonly ICountryService _countryService;

    public RegistrationService(
        IUserRepository userRepository, IUserIdentityRepository userIdentityRepository, 
        IEmailService emailService, IPasswordHashService passwordHashService, 
        IConfirmationGenerator confirmationGenerator, IOptions<SenderSettings> senderSettings,
        ICountryService countryService)
    {
        _userRepository = userRepository;
        _userIdentityRepository = userIdentityRepository;
        _emailService = emailService;
        _passwordHashService = passwordHashService;
        _confirmationGenerator = confirmationGenerator;
        _countryService = countryService;
        _senderSettings = senderSettings.Value;
    }

    private string GetConfirmationEmailBody(int confirmationCode)
    {
        return $"Your confirmation code is {confirmationCode}";
    }
    
    public async Task<Result> RegisterAsync(RegistrationRequest request)
    {
        bool userExists = await _userIdentityRepository.ExistsAsync(request.Email);
        
        if (userExists)
        {
            return RegistrationErrors.AlreadyRegistered;
        }

        string recipientName = $"{request.LastName} {request.FirstName}";
        int confirmationCode = _confirmationGenerator.GenerateCode();
        string emailBody = GetConfirmationEmailBody(confirmationCode);
        
        EmailMessage confirmationEmail = new EmailMessage()
        {
            FromAddress = _senderSettings.FromAddress,
            FromName = _senderSettings.FromName,
            Subject = _subject,
            ToAddress = request.Email,
            ToName = recipientName,
            Body = emailBody
        };

        try
        {
            await _emailService.SendEmailAsync(confirmationEmail);
        }
        catch
        {
            return RegistrationErrors.EmailConfirmationNotSent;
        }

        User newUser = request.CreateAppUser(confirmationCode, _passwordHashService, _countryService);
        
        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveAsync();
        
        return Result.Success();
    }

    public async Task<Result> ConfirmRegistrationAsync(string email, int confirmationCode)
    {
        UserIdentity? identity = await _userIdentityRepository.GetByEmailAsync(email);

        if (identity is null)
        {
            return CodeConfirmationErrors.NoEmail;
        }
        
        ConfirmationCode? storedCode = identity.ConfirmationCode;

        if (storedCode is null)
        {
            return identity.EmailConfirmed 
                ? CodeConfirmationErrors.AlreadyConfirmed
                : throw new InvalidOperationException("No confirmation code");
        }

        if (storedCode.ExpireAt.CompareTo(DateTime.UtcNow) < 0)
        {
            return CodeConfirmationErrors.Expired;
        }

        if (storedCode.Code != confirmationCode)
        {
            return CodeConfirmationErrors.NoMatch;
        }
        
        identity.EmailConfirmed = true;
        _userIdentityRepository.SetConfirmationCode(identity, null);
        _userIdentityRepository.Update(identity, nameof(UserIdentity.EmailConfirmed));
        await _userIdentityRepository.SaveAsync();

        return Result.Success();
    }
}
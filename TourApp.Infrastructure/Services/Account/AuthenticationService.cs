namespace TourApp.Infrastructure.Services.Account;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;

    public AuthenticationService(
        IUserRepository userRepository, IPasswordHashService passwordHashService)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
    }

    private bool ComparePasswordBytes(byte[] password1, byte[] password2)
    {
        return password1.Length == password2.Length 
               && password1.SequenceEqual(password2);
    }
    
    public async Task<Result<AuthenticationResponse?>> AuthenticateWithPasswordAsync(AuthenticationRequest request)
    {
        User? appUser = await _userRepository.GetByEmailAsync(request.Email);

        if (appUser is null)
        {
            return AuthenticationErrors.IncorrectEmail;
        }
        
        UserIdentity userIdentity = appUser.Identity;
        
        if (!userIdentity.EmailConfirmed)
        {
            return AuthenticationErrors.NotConfirmed;
        }
        
        byte[] requestPasswordBytes = Encoding.ASCII.GetBytes(request.Password);
        byte[] requestPasswordHash = _passwordHashService
            .ComputeHash(requestPasswordBytes, userIdentity.PasswordSalt);

        if (!ComparePasswordBytes(requestPasswordHash, userIdentity.PasswordHash))
        {
            return AuthenticationErrors.IncorrectPassword;
        }
        
        AuthenticationResponse response = new AuthenticationResponse(appUser.Id, appUser.Role.Name);
        
        return Result<AuthenticationResponse>.Success(response);
    }
}
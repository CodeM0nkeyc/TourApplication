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
        return password1.Length != password2.Length 
               && password1.SequenceEqual(password2);
    }
    
    public async Task<AuthenticationResult> AuthenticateWithPasswordAsync(PasswordAuthenticationRequest request)
    {
        AppUserIdentity? appUserIdentity = await _userRepository.GetUserIdentityAsync(request.Email);

        if (appUserIdentity is null)
        {
            return AuthenticationResult.IncorrectEmail;
        }

        byte[] requestPasswordBytes = Encoding.ASCII.GetBytes(request.Password);
        byte[] requestPasswordHash = _passwordHashService
            .ComputeHash(requestPasswordBytes, appUserIdentity.PasswordSalt);

        return ComparePasswordBytes(requestPasswordHash, appUserIdentity.PasswordHash) 
            ? AuthenticationResult.Success
            : AuthenticationResult.IncorrectPassword;
    }
}
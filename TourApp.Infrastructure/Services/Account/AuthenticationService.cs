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
    
    public async Task<AuthenticationResponse> AuthenticateWithPasswordAsync(AuthenticationRequest request)
    {
        AppUser? appUser = await _userRepository.GetByEmailAsync(request.Email);

        if (appUser is null)
        {
            return new AuthenticationResponse(AuthenticationResult.IncorrectEmail);
        }
        
        AppUserIdentity appUserIdentity = appUser.Identity;
        
        if (!appUserIdentity.EmailConfirmed)
        {
            return new AuthenticationResponse(AuthenticationResult.NotConfirmed);
        }
        
        byte[] requestPasswordBytes = Encoding.ASCII.GetBytes(request.Password);
        byte[] requestPasswordHash = _passwordHashService
            .ComputeHash(requestPasswordBytes, appUserIdentity.PasswordSalt);

        AuthenticationResult authenticationResult = 
            ComparePasswordBytes(requestPasswordHash, appUserIdentity.PasswordHash) 
            ? AuthenticationResult.Success
            : AuthenticationResult.IncorrectPassword;

        return new AuthenticationResponse(authenticationResult, appUser.Id, appUser.Role.Name);
    }
}
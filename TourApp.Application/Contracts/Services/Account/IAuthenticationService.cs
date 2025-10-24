namespace TourApp.Application.Contracts.Services.Account;

public interface IAuthenticationService
{
    public Task<AuthenticationResult> AuthenticateWithPasswordAsync(PasswordAuthenticationRequest request);
}
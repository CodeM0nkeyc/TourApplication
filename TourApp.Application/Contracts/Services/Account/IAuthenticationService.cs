namespace TourApp.Application.Contracts.Services.Account;

public interface IAuthenticationService
{
    public Task<AuthenticationResponse> AuthenticateWithPasswordAsync(AuthenticationRequest request);
}
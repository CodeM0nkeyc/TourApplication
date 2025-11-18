namespace TourApp.Application.Contracts.Services.Account;

public interface IAuthenticationService
{
    public Task<Result<AuthenticationResponse?>> AuthenticateWithPasswordAsync(
        AuthenticationRequest request, CancellationToken cancellationToken = default);
}
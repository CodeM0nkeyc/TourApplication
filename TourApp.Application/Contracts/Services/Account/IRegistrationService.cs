namespace TourApp.Application.Contracts.Services.Account;

public interface IRegistrationService
{
    public Task<RegistrationResult> RegisterAsync(RegistrationRequest request);
    public Task<ConfirmationResult> ConfirmRegistrationAsync(string email, int confirmationCode);
}
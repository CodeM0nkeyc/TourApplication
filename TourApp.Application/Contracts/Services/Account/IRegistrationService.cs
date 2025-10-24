namespace TourApp.Application.Contracts.Services.Account;

public interface IRegistrationService
{
    public Task RegisterAsync(RegistrationRequest request);
    public Task ConfirmRegistrationAsync(int confirmationCode);
}
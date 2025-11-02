namespace TourApp.Application.Contracts.Services.Account;

public interface IRegistrationService
{
    public Task<Result> RegisterAsync(RegistrationRequest request);
    public Task<Result> ConfirmRegistrationAsync(string email, int confirmationCode);
}
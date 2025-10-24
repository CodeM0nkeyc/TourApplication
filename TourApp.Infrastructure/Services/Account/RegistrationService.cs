using TourApp.Application.Models.Registration;

namespace TourApp.Infrastructure.Services.Account;

public class RegistrationService : IRegistrationService
{
    public Task RegisterAsync(RegistrationRequest request)
    {
        throw new NotImplementedException();
    }

    public Task ConfirmRegistrationAsync(int confirmationCode)
    {
        throw new NotImplementedException();
    }
}
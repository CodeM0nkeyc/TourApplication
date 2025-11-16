namespace TourApp.Infrastructure.Extensions;

public static class RegistrationRequestExtensions
{
    public static User CreateAppUser(
        this RegistrationRequest request, int confirmationCode, 
        IPasswordHashService passwordHashService, ICountryService countryService)
    {
        byte[] passwordBytes = Encoding.ASCII.GetBytes(request.Password);
        byte[] salt = passwordHashService.GenerateSalt();

        byte[] passwordHash = passwordHashService.ComputeHash(passwordBytes, salt);

        string? dialCode = null;

        if (request.PhoneNumber is not null)
        {
            dialCode = countryService.GetCountryDialCode(request.Address.Country);
        }

        UserIdentity identity = new UserIdentity()
        {
            Email = request.Email,
            DialCode = dialCode,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = passwordHash,
            PasswordSalt = salt,
            ConfirmationCode = new ConfirmationCode()
            {
                Code = confirmationCode,
                ExpireAt = DateTime.UtcNow.AddMinutes(10)
            }
        };
        
        User user = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Address = request.Address,
            RoleId = (int)Role.Customer,
            Identity = identity
        };

        return user;
    }
}
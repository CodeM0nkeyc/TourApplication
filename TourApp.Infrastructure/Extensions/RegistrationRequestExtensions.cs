namespace TourApp.Infrastructure.Extensions;

public static class RegistrationRequestExtensions
{
    public static AppUser CreateAppUser(
        this RegistrationRequest request, int confirmationCode, IPasswordHashService passwordHashService)
    {
        byte[] passwordBytes = Encoding.ASCII.GetBytes(request.Password);
        byte[] salt = passwordHashService.GenerateSalt();

        byte[] passwordHash = passwordHashService.ComputeHash(passwordBytes, salt);

        AppUserRole role = new AppUserRole() { Id = (int)Role.Customer };

        AppUserIdentity identity = new AppUserIdentity()
        {
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = passwordHash,
            PasswordSalt = salt,
            ConfirmationCode = new ConfirmationCode()
            {
                Code = confirmationCode,
                ExpireAt = DateTime.UtcNow.AddMinutes(10)
            }
        };
        
        AppUser user = new AppUser()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Address = request.Address,
            Role = role,
            Identity = identity
        };

        return user;
    }
}
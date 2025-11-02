namespace TourApp.Application.Models.Registration;

public static class RegistrationErrors
{
    public const string Prefix = "Registration";
    
    public static readonly Error AlreadyRegistered = 
        new Error($"{Prefix}.{nameof(AlreadyRegistered)}", "User is already registered");
    
    public static readonly Error EmailConfirmationNotSent = 
        new Error($"{Prefix}.{nameof(EmailConfirmationNotSent)}", "Error occured while sending confirmation email");
}
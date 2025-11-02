namespace TourApp.Application.Models.Authentication;

public class AuthenticationErrors
{
    public const string Prefix = "Authentication";
    
    public static readonly Error IncorrectEmail = 
        new Error($"{Prefix}.{nameof(IncorrectEmail)}", "Account with such email doesn't exist");
    
    public static readonly Error IncorrectPassword = 
        new Error($"{Prefix}.{nameof(IncorrectPassword)}", "Incorrect password");
    
    public static readonly Error NotConfirmed = 
        new Error($"{Prefix}.{nameof(NotConfirmed)}", "Account email is not confirmed");
}
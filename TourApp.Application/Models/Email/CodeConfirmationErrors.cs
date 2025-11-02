namespace TourApp.Application.Models.Email;

public static class CodeConfirmationErrors
{
    public const string CodePrefix = "CodeConfirmation";
    
    public static readonly Error NoMatch = 
        new Error($"{CodePrefix}.{nameof(NoMatch)}", "Code doesn't match");
    
    public static readonly Error Expired = 
        new Error($"{CodePrefix}.{nameof(Expired)}", "Confirmation code is expired");
    
    public static readonly Error AlreadyConfirmed = 
        new Error($"{CodePrefix}.{nameof(AlreadyConfirmed)}", "Confirmation code is already used");
    
    public static readonly Error NoEmail = 
        new Error($"{CodePrefix}.{nameof(NoEmail)}", "Account with this email doesn't exist");
}
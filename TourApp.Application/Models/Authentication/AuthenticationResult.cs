namespace TourApp.Application.Models.Authentication;

public enum AuthenticationResult
{
    Success,
    IncorrectEmail,
    IncorrectPassword,
    NotConfirmed
}
namespace TourApp.Application.Models.Email;

public enum ConfirmationResult
{
    Success,
    NoMatch,
    Expired,
    AlreadyConfirmed
}
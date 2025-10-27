namespace TourApp.Application.Exceptions;

public class NoConfirmationEmailException : Exception
{
    public string Email { get; }
    public NoConfirmationEmailException(string email) : base($"Email {email} is not registered")
    {
        Email = email;
    }
}
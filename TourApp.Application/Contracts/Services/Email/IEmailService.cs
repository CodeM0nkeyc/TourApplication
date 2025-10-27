namespace TourApp.Application.Contracts.Services.Email;

public interface IEmailService
{
    public Task SendEmailAsync(EmailMessage message);
}
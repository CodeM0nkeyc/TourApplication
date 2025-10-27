using MailKit.Security;

namespace TourApp.Infrastructure.Services.Email;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }
    
    public async Task SendEmailAsync(EmailMessage message)
    {
        MimeMessage emailMessage = new MimeMessage()
        {
            From = { new MailboxAddress(message.FromName, message.FromAddress) },
            To = { new MailboxAddress(message.ToName, message.ToAddress) },
            Subject = message.Subject,
            Body = new TextPart(TextFormat.Plain)
            {
                Text = message.Body
            }
        };

        using SmtpClient client = new SmtpClient();
        
        await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}
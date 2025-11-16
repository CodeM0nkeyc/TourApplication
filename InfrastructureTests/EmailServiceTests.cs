using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using TourApp.Application.Contracts.Services.Email;
using TourApp.Application.Models.Email;
using TourApp.Infrastructure.Models;
using TourApp.Infrastructure.Services.Email;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace InfrastructureTests;

public class EmailServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public EmailServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task SendEmail()
    {
        SmtpSettings smtpSettings = new SmtpSettings()
        {
            Host = "in-v3.mailjet.com",
            Port = 587,
            Username = "fb82114ed0d6a0876943d0bf718d9c51",
            Password = "27e4c6717cb075eadf06d422862ea510"
        };

        SenderSettings senderSettings = new SenderSettings()
        {
            FromAddress = "vangazs35@gmail.com",
            FromName = "Anonym"
        };

        int num = RandomNumberGenerator.GetInt32(Int32.MinValue, Int32.MaxValue);

        IEmailService emailService = new EmailService(new OptionsWrapper<SmtpSettings>(smtpSettings));
        EmailMessage message = new EmailMessage()
        {
            FromAddress = senderSettings.FromAddress,
            FromName = senderSettings.FromName,
            Subject = "Unit test",
            ToAddress = "somegames748@gmail.com",
            ToName = "Anton",
            Body = "Hello from test project with number: " + num
        };

        try
        {
            await emailService.SendEmailAsync(message);
            Assert.True(true);
        }
        catch(Exception ex)
        {
            _testOutputHelper.WriteLine(ex.Message);
            Assert.True(false);
        }
    }
}
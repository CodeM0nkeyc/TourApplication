namespace TourApp.Infrastructure.Extensions;

public static class InfrastructureDiExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration emailConfiguration)
    {
        services.AddSingleton<IPasswordHashService, Pbkdf2PasswordHashService>();
        services.AddSingleton<IConfirmationGenerator, ConfirmationGenerator>();
        services.AddSingleton<IEmailService, EmailService>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IRegistrationService, RegistrationService>();

        services.Configure<SmtpSettings>(opts =>
        {
            IConfigurationSection smtpSection = emailConfiguration.GetSection("SmtpSettings");
            
            opts.Host = smtpSection["Host"] 
                        ?? throw new InvalidOperationException("Missing host");
            opts.Port = int.Parse(smtpSection["Port"] 
                                  ?? throw new InvalidOperationException("Missing port"));
            
            opts.Username = smtpSection["Username"] 
                            ?? throw new InvalidOperationException("Missing username");
            opts.Password = smtpSection["Password"] 
                            ?? throw new InvalidOperationException("Missing password");
        });

        services.Configure<SenderSettings>(opts =>
        {
            IConfigurationSection senderSection = emailConfiguration.GetSection("SenderSettings");
            
            opts.FromAddress = senderSection["FromAddress"]
                               ?? throw new InvalidOperationException("Missing sender address");
            opts.FromName = senderSection["FromName"]
                            ?? throw new InvalidOperationException("Missing sender name");
        });
        
        return services;
    }
}
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
        
        services.AddScoped<IUserService, UserService>();

        services.Configure<SmtpSettings>(opts =>
        {
            opts.Host = emailConfiguration["Host"] 
                ?? throw new InvalidOperationException("Missing host");
            opts.Port = int.Parse(emailConfiguration["Port"] 
                ?? throw new InvalidOperationException("Missing port"));
            
            opts.Username = emailConfiguration["Username"] 
                ?? throw new InvalidOperationException("Missing username");
            opts.Password = emailConfiguration["Password"] 
                ?? throw new InvalidOperationException("Missing password");
        });

        services.Configure<SenderSettings>(opts =>
        {
            opts.FromAddress = emailConfiguration["FromAddress"]
                               ?? throw new InvalidOperationException("Missing sender address");
            opts.FromName = emailConfiguration["FromName"]
                            ?? throw new InvalidOperationException("Missing sender name");
        });
        
        return services;
    }
}
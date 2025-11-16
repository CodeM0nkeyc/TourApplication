namespace TourApp.Infrastructure.Extensions;

public static class InfrastructureDiExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration emailConfiguration)
    {
        services.AddSingleton<IPasswordHashService, Pbkdf2PasswordHashService>();
        services.AddSingleton<IConfirmationGenerator, ConfirmationGenerator>();
        services.AddSingleton<IEmailService, EmailService>();
        services.AddSingleton<ICountryService, CountryService>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IRegistrationService, RegistrationService>();

        services.Configure<SmtpSettings>(
            opts => emailConfiguration.GetSection("SmtpSettings").Bind(opts));

        services.Configure<SenderSettings>(
            opts => emailConfiguration.GetSection("SenderSettings").Bind(opts));
        
        return services;
    }
}
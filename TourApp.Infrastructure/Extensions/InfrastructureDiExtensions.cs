namespace TourApp.Infrastructure.Extensions;

public static class InfrastructureDiExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHashService, Pbkdf2PasswordHashService>();
        services.AddSingleton<IConfirmationGenerator, ConfirmationGenerator>();

        services.Configure<SmtpSettings>(opts =>
        {
            opts.Host = configuration["Host"] ?? throw new InvalidOperationException("Missing host");
            opts.Port = int.Parse(configuration["Port"] ?? throw new InvalidOperationException("Missing port"));
            
            opts.Username = configuration["Username"] ?? throw new InvalidOperationException("Missing username");
            opts.Password = configuration["Password"] ?? throw new InvalidOperationException("Missing password");
        });
        
        return services;
    }
}
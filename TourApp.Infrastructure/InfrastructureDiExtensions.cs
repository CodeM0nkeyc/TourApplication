namespace TourApp.Infrastructure;

public static class InfrastructureDiExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHashService, Pbkdf2PasswordHashService>();
        
        return services;
    }
}
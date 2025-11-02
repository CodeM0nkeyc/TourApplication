namespace TourApp.Application.Extensions;

public static class ApplicationDiExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationDiExtensions).Assembly));
        services.AddAutoMapper(typeof(ApplicationDiExtensions).Assembly);
        services.AddValidatorsFromAssembly(typeof(ApplicationDiExtensions).Assembly);
        
        services.AddSingleton<TourSpecificationFactory>();
        services.AddSingleton<UserSpecificationFactory>();

        services.AddSingleton<CountryService>();
        
        return services;
    }
}
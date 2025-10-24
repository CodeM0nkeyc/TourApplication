using FluentValidation;
using TourApp.Application.Features.Users.Specifications;

namespace TourApp.Application;

public static class ApplicationDiExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationDiExtensions).Assembly));
        services.AddAutoMapper(typeof(ApplicationDiExtensions).Assembly);
        services.AddValidatorsFromAssembly(typeof(ApplicationDiExtensions).Assembly);
        
        services.AddSingleton<TourSpecificationFactory>();
        services.AddSingleton<UserSpecificationFactory>();
        
        return services;
    }
}
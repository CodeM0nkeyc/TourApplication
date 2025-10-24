using TourApp.Application.Features.Tours.Contracts.Repositories;

namespace TourApp.Persistence;

public static class PersistenceDiExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddDbContext<ApplicationDbContext>(opts =>
        {
            opts.UseSqlServer(configuration.GetConnectionString("TourDb") 
                              ?? throw new InvalidConfigurationException("There is no connection string for TourDb"))
                .EnableSensitiveDataLogging(isDevelopment);
        });

        services.AddScoped<ITourRepository, TourRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}
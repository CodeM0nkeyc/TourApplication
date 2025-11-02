namespace TourApp.Persistence.Extensions;

public static class PersistenceDiExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddDbContext<ApplicationDbContext>(opts =>
        {
            opts.UseSqlServer(configuration.GetConnectionString("TourDb") 
                              ?? throw new InvalidOperationException("There is no connection string for TourDb"))
                .EnableSensitiveDataLogging(isDevelopment);
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ITourRepository, TourRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserIdentityRepository, UserIdentityRepository>();
        
        return services;
    }
}
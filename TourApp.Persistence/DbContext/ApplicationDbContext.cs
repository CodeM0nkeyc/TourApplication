namespace TourApp.Persistence.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourPricingDetails> TourPricingDetails { get; set; }
    
    public DbSet<TourBooking> TourBooking { get; set; }
    public DbSet<TourCustomer> TourCustomers { get; set; }
    
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppUserIdentity> AppUserIdentities { get; set; }
    public DbSet<AppUserRole> AppUserRoles { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 4);

        configurationBuilder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter, DateOnlyComparer>()
            .HaveColumnType("date");
    }
}
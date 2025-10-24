namespace TourApp.Persistence.Repositories;

public class UserRepository : GenericRepository<AppUser, Guid>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<AppUser?> GetByEmailAsync(string email)
    {
        AppUser? appUser = await dbContext.AppUsers
            .Include(user => user.Identity)
            .SingleOrDefaultAsync(user => user.Identity.Email == email);
        return appUser;
    }

    public async Task<AppUserIdentity?> GetUserIdentityAsync(string email)
    {
        AppUserIdentity? appUserIdentity = await dbContext.AppUserIdentities
            .Where(user => user.Email == email)
            .FirstOrDefaultAsync();
        
        return appUserIdentity;
    }

    public async Task<AppUser?> GetByPhoneNumberAsync(string phoneNumber)
    {
        AppUser? appUser = await dbContext.AppUsers
            .Include(user => user.Identity)
            .SingleOrDefaultAsync(user => user.Identity.PhoneNumber == phoneNumber);
        
        return appUser;
    }
}
namespace TourApp.Persistence.Repositories;

public class UserIdentityRepository : IUserIdentityRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IGenericRepository<UserIdentity> _genericRepository;

    public UserIdentityRepository(
        ApplicationDbContext dbContext, IGenericRepository<UserIdentity> genericRepository)
    {
        _dbContext = dbContext;
        _genericRepository = genericRepository;
        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    public async Task<UserIdentity?> GetByIdAsync(int id)
    {
        UserIdentity? userIdentity = await _dbContext.UserIdentities
            .FirstOrDefaultAsync(identity => identity.Id == id);

        return userIdentity;
    }

    public async Task<UserIdentity?> GetByEmailAsync(string email)
    {
        UserIdentity? userIdentity = await _dbContext.UserIdentities
            .FirstOrDefaultAsync(identity => identity.Email == email);

        return userIdentity;
    }

    public async Task<bool> ExistsAsync(string email)
    {
        bool exists = await _dbContext.UserIdentities.AnyAsync(identity => identity.Email == email);
        
        return exists;
    }

    public void Update(UserIdentity userIdentity)
    {
        _genericRepository.Update(userIdentity);
    }


    public void Update(UserIdentity userIdentity, params string[] props)
    {
        _genericRepository.Update(userIdentity, props);
    }

    public void SetConfirmationCode(UserIdentity userIdentity, ConfirmationCode? confirmationCode)
    {
        if (_dbContext.Entry(userIdentity).State == EntityState.Detached)
        {
            _dbContext.Attach(userIdentity);
        }
        
        userIdentity.ConfirmationCode = confirmationCode;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
namespace TourApp.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        User? user = await dbContext.Users
            .Include(user => user.Identity)
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Identity.Email == email, cancellationToken);
        return user;
    }

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        User? user = await dbContext.Users
            .Include(user => user.Identity)
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Identity.PhoneNumber == phoneNumber, cancellationToken);
        
        return user;
    }
}
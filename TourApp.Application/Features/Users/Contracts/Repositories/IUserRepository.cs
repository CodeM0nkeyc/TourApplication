namespace TourApp.Application.Features.Users.Contracts.Repositories;

public interface IUserRepository : IGenericRepository<AppUser, Guid>
{
    public Task<AppUser?> GetByEmailAsync(string email);
    
    public Task<AppUserIdentity?> GetUserIdentityAsync(string email);
}
namespace TourApp.Application.Features.Users.Contracts.Repositories;

public interface IUserRepository : IGenericRepository<AppUser>
{
    public Task<AppUser?> GetByEmailAsync(string email);
    public Task<AppUser?> GetByPhoneNumberAsync(string phoneNumber);
    
    public Task<AppUserIdentity?> GetUserIdentityByIdAsync(int id);
    public Task<AppUserIdentity?> GetUserIdentityByEmailAsync(string email);
}
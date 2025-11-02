using TourApp.Application.Contracts.Repositories;

namespace TourApp.Application.Features.Users.Contracts.Repositories;

public interface IUserIdentityRepository : IUnitOfWork
{
    public Task<UserIdentity?> GetByIdAsync(int id);
    public Task<UserIdentity?> GetByEmailAsync(string email);

    public Task<bool> ExistsAsync(string email);
    
    public void Update(UserIdentity userIdentity);
    public void Update(UserIdentity userIdentity, params string[] props);
    
    public void SetConfirmationCode(UserIdentity userIdentity, ConfirmationCode? confirmationCode);
}
namespace TourApp.Application.Features.Users.Contracts.Repositories;

public interface IUserIdentityRepository : IUnitOfWork
{
    public Task<UserIdentity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<UserIdentity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    public Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default);
    
    public void Update(UserIdentity userIdentity);
    public void Update(UserIdentity userIdentity, params string[] props);
    
    public void SetConfirmationCode(UserIdentity userIdentity, ConfirmationCode? confirmationCode);
}
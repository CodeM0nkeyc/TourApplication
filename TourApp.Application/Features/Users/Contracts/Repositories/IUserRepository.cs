namespace TourApp.Application.Features.Users.Contracts.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    public Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
}
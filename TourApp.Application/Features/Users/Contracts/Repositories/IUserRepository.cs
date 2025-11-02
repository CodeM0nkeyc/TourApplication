using TourApp.Application.Contracts.Repositories;

namespace TourApp.Application.Features.Users.Contracts.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User?> GetByEmailAsync(string email);
    public Task<User?> GetByPhoneNumberAsync(string phoneNumber);
}
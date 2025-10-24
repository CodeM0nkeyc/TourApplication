namespace TourApp.Infrastructure.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserSpecificationFactory _userSpecificationFactory;

    public UserService(IUserRepository userRepository, UserSpecificationFactory userSpecificationFactory)
    {
        _userRepository = userRepository;
        _userSpecificationFactory = userSpecificationFactory;
    }
    
    public async Task<bool> ChangeEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangePasswordAsync(string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangePhoneNumberAsync(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UserExists(string email)
    {
        return await _userRepository.GetByEmailAsync(email) is not null;
    }

    public async Task<bool> CheckUniquenessAsync(string email, string? phoneNumber)
    {
        Specification<AppUser> spec = _userSpecificationFactory.CreateSpecification(new UserQuerySettings()
        {
            Email = email,
            PhoneNumber = phoneNumber
        })!;
        
        int count = await _userRepository.CountAsync(spec);

        return count < 1;
    }
}
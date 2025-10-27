namespace TourApp.Application.Features.Users.Contracts.Services;

public interface IUserService
{
    public Task<bool> ChangeEmailAsync(string email);
    public Task<bool> ChangePasswordAsync(string oldPassword, string newPassword);
    public Task<bool> ChangePhoneNumberAsync(string phoneNumber);
    
    public Task<bool> UserWithEmailExistsAsync(string email);
    
    public Task<bool> CheckUniquenessAsync(string email, string? phoneNumber);
}
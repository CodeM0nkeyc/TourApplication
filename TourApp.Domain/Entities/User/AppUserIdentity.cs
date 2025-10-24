namespace TourApp.Domain.Entities;

public class AppUserIdentity : EntityBase<int>
{
    public string Email;
    public string? PhoneNumber;

    public byte[] PasswordHash;
    public byte[] PasswordSalt;
    
    public AppUser User { get; set; }
}
namespace TourApp.Domain.Entities.User;

public class AppUserIdentity : EntityBase<int>
{
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }

    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    
    public bool EmailConfirmed { get; set; }
    
    public ConfirmationCode? ConfirmationCode { get; set; }
    
    public AppUser User { get; set; }
}
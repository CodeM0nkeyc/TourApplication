namespace TourApp.Domain.Entities.User;

public class UserIdentity : EntityBase<int>
{
    public string Email { get; set; }
    
    public string? DialCode { get; set; }
    public string? PhoneNumber { get; set; }

    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    
    public bool EmailConfirmed { get; set; }
    
    public ConfirmationCode? ConfirmationCode { get; set; }
    
    public User User { get; set; }
}
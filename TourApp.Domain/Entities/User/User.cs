namespace TourApp.Domain.Entities.User;

public class User : EntityBase<int>
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    
    public Address Address { get; set; }
    
    public UserIdentity Identity { get; set; }
    
    public int RoleId { get; set; }
    public UserRole Role { get; set; }
    
    public byte[]? Image { get; set; }
    
    public ICollection<TourBooking> TourBookings { get; set; }
}
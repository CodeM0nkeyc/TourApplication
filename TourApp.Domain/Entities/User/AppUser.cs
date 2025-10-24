using TourApp.Domain.Entities.Booking;

namespace TourApp.Domain.Entities.User;

public class AppUser : EntityBase<int>
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    
    public Address Address { get; set; }
    
    public AppUserIdentity Identity { get; set; }
    
    public AppUserRole Role { get; set; }
    
    public byte[]? Image { get; set; }
    
    public ICollection<TourBooking> TourBookings { get; set; }
}
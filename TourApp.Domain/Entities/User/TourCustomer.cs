using TourApp.Domain.Entities.Booking;

namespace TourApp.Domain.Entities.User;

public class TourCustomer : EntityBase<int>
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    
    public int Age { get; set; }
    
    public ICollection<TourBooking> TourBookings { get; set; }
}
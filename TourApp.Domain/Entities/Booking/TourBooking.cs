using TourApp.Domain.Entities.Booking.Common;

namespace TourApp.Domain.Entities.Booking;

public class TourBooking : EntityBase<int>
{
    public DateTime BookingDate { get; set; }
    public DateTime? PayedDate { get; set; }
    public DateTime? CancelledDate { get; set; }
    
    public int CustomersCount { get; set; }
    
    public decimal Price { get; set; }
    public BookingState State { get; set; }
    
    public Tour.Tour Tour { get; set; }
    
    public User.User User { get; set; }
    
    public ICollection<TourCustomer> TourCustomers { get; set; }
}
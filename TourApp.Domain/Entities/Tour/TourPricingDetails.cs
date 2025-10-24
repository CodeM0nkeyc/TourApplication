namespace TourApp.Domain.Entities.Tour;

public class TourPricingDetails : EntityBase<int>
{
    public decimal? EquipmentRentPrice { get; set; }
    public decimal? GuideFee { get; set; }
    public decimal? TransportationFee { get; set; }
    public decimal? AccommodationFee { get; set; }
    public decimal? MealFee { get; set; }
}
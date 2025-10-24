using TourApp.Domain.Entities.Base;
using TourApp.Domain.Entities.Tour.Common;

namespace TourApp.Domain.Entities.Tour;

public class Tour : EntityBase<int>
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public TourState State { get; set; }
    
    public string Heading { get; set; }
    public string Country { get; set; }
    public string Description { get; set; }
    public TourDifficulty? Difficulty { get; set; }
    public int RemainingPlaces { get; set; }
    public float? Rating { get; set; }
    public DateOnly? StartDate { get; set; }
    public decimal Price { get; set; }
    public int? DiscountPercent { get; set; }
    public int StopCount { get; init; }
    public int DurationInDays { get; init; }
    
    public string DisplayImageName { get; set; }
    
    public TourPricingDetails TourPricingDetails { get; set; }
}
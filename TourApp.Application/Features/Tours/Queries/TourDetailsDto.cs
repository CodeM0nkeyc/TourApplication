namespace TourApp.Application.Features.Tours.Queries;

public record TourDetailsDto
{
    public string Heading { get; init; }
    public string Country { get; init; }
    public string Description { get; init; }
    public string Difficulty { get; init; }
    public int RemainingPlaces { get; init; }
    public float? Rating { get; init; }
    
    public int StopCount { get; init; }
    public int DurationInDays { get; init; }
    public DateOnly? StartDate { get; init; }
    public decimal Price { get; init; }
    
    public string ImageSrc { get; init; }
}
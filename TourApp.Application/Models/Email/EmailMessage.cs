namespace TourApp.Application.Models.Email;

public record EmailMessage
{
    public string FromAddress { get; init; }
    public string FromName { get; init; }
    
    public string ToAddress { get; init; }
    public string ToName { get; init; }
    
    public string? Subject { get; init; }
    public string Body { get; init; }
}
namespace TourApp.Application.Models.Email;

public record EmailMessage(
    string From,
    string To,
    string? Subject,
    string Body
);
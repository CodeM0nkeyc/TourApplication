namespace TourApp.Application.Models.Authentication;

public record AuthenticationResponse(
    int? UserId = null,
    Role? UserRole = null
);
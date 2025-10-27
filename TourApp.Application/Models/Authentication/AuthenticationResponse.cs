namespace TourApp.Application.Models.Authentication;

public record AuthenticationResponse(
    AuthenticationResult AuthenticationResult,
    int? UserId = null,
    Role? UserRole = null
);
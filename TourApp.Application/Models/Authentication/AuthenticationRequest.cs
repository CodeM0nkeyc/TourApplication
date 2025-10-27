namespace TourApp.Application.Models.Authentication;

public record AuthenticationRequest(
    string Email,
    string Password
);
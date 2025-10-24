namespace TourApp.Application.Models.Authentication;

public record PasswordAuthenticationRequest(
    string Email,
    string Password
);
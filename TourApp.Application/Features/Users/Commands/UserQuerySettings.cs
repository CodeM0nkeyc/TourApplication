namespace TourApp.Application.Features.Users.Commands;

public record UserQuerySettings(
    int? Id = null,
    string? Email = null,
    string? PhoneNumber = null
);
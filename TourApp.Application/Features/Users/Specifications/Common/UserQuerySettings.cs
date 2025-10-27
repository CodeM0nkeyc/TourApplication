namespace TourApp.Application.Features.Users.Specifications.Common;

public record UserQuerySettings(
    int? Id = null,
    string? Email = null,
    string? PhoneNumber = null
);
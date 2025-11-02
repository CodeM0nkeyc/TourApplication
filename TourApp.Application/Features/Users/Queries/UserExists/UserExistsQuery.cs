namespace TourApp.Application.Features.Users.Queries.UserExists;

public record UserExistsQuery(string Email) : IRequest<bool>;
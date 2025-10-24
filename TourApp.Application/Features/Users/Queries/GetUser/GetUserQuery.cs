namespace TourApp.Application.Features.Users.Queries.GetUser;

public record GetUserQuery(
    string Email,
    Guid? Id
) : IRequest<AppUserDto?>;
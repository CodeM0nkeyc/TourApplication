namespace TourApp.Application.Features.Users.Queries.UserExists;

public class UserExistsQueryHandler : IRequestHandler<UserExistsQuery, bool>
{
    private readonly IUserIdentityRepository _userIdentityRepository;

    public UserExistsQueryHandler(IUserIdentityRepository userIdentityRepository)
    {
        _userIdentityRepository = userIdentityRepository;
    }
    
    public async Task<bool> Handle(UserExistsQuery request, CancellationToken cancellationToken)
    {
        bool exists = await _userIdentityRepository.ExistsAsync(request.Email);
        
        return exists;
    }
}
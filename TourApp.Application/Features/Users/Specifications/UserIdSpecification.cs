namespace TourApp.Application.Features.Users.Specifications;

public class UserIdSpecification : Specification<AppUser>
{
    public UserIdSpecification(int id) : base(user => user.Id == id)
    { }
}
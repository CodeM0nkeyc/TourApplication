namespace TourApp.Application.Features.Users.Specifications;

public class UserEmailSpecification : Specification<AppUser>
{
    public UserEmailSpecification(string email)
    {
        AddInclude(user => user.Identity);
        Filter.And(user => user.Identity.Email == email);
    }
}
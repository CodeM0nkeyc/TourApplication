namespace TourApp.Application.Features.Users.Specifications;

public class UserEmailSpecification : Specification<User>
{
    public UserEmailSpecification(string email)
    {
        AddInclude(user => user.Identity);
        Filter.And(user => user.Identity.Email == email);
    }
}
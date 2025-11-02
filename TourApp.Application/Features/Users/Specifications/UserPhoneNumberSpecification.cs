namespace TourApp.Application.Features.Users.Specifications;

public class UserPhoneNumberSpecification : Specification<User>
{
    public UserPhoneNumberSpecification(string phoneNumber)
    {
        AddInclude(user => user.Identity);
        Filter.And(user => user.Identity.Email == phoneNumber);
    }
}
namespace TourApp.Application.Features.Users.Specifications;

public class UserPhoneNumberSpecification : Specification<AppUser>
{
    public UserPhoneNumberSpecification(string phoneNumber)
    {
        AddInclude(user => user.Identity);
        Filter.And(user => user.Identity.Email == phoneNumber);
    }
}
using TourApp.Application.Features.Users.Specifications.Common;

namespace TourApp.Application.Features.Users.Specifications;

public class UserSpecificationFactory : SpecificationFactory<User, UserQuerySettings>
{
    public override Specification<User>? CreateSpecification(UserQuerySettings? settings)
    {
        if (settings is null)
        {
            return null;
        }

        Specification<User> spec = new Specification<User>();

        if (settings.Id.HasValue)
        {
            spec.And(new UserIdSpecification(settings.Id.Value));
        }

        if (settings.Email is not null)
        {
            spec.And(new UserEmailSpecification(settings.Email));
        }

        return spec;
    }
}
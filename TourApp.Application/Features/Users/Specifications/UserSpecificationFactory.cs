using TourApp.Application.Features.Users.Commands;

namespace TourApp.Application.Features.Users.Specifications;

public class UserSpecificationFactory : SpecificationFactory<AppUser, UserQuerySettings>
{
    public override Specification<AppUser>? CreateSpecification(UserQuerySettings? settings)
    {
        if (settings is null)
        {
            return null;
        }

        Specification<AppUser> spec = new Specification<AppUser>();

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
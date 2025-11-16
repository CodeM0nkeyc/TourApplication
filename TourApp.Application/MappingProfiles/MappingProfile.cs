namespace TourApp.Application.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tour, TourDetailsDto>()
            .ForMember(dest => dest.ImageSrc, opts => opts.MapFrom(src =>
                    src.Id + "/" + src.DisplayImageName));

        CreateMap<User, UserDto>().ReverseMap();
    }
}
using AutoMapper;
using PersonalSite.Api.Dtos;
using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Api.Mappings;

public class ProfileProfile : Profile
{
    public ProfileProfile()
    {
        CreateMap<ProfileEntity, ProfileDto>()
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName))
            .ForMember(x => x.NickName, y => y.MapFrom(z => z.Nickname))
            .ForMember(x => x.ProfilePicture, y => y.MapFrom(z => z.ProfilePicture))
            .ForMember(x => x.Token, y => y.Ignore());
    }
}
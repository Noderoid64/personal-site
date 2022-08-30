using AutoMapper;
using PersonalSite.Api.Dtos;
using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Api.Mappings;

public class PostRecentProfile : Profile
{
    public PostRecentProfile()
    {
        CreateMap<FileObjectEntity, PostRecentDto>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
            .ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
            .ForMember(x => x.AuthorNickname, y => y.MapFrom(z => z.Profile.Nickname))
            .ForMember(x => x.AuthorPicture, y => y.MapFrom(z => z.Profile.ProfilePicture))
            .ForMember(x => x.CreatedAt, y => y.MapFrom(z => z.CreatedAt));
    }
}
using AutoMapper;
using PersonalSite.Api.Dtos;
using PersonalSite.Core.Entities;

namespace PersonalSite.Api.Mappings;

public class PostProfile:Profile
{
    public PostProfile()
    {
        CreateMap<PostEntity, PostDto>()
            .ReverseMap()
            .ForMember(x => x.Profile, y => y.Ignore());
    }
}
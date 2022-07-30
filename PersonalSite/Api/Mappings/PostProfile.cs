using AutoMapper;
using PersonalSite.Api.Dtos;
using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Api.Mappings;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<PostDto, FileObjectEntity>()
            .ForMember(x => x.Content, y => y.MapFrom(z => z.Content))
            .ForMember(x => x.PostAccessType, y => y.MapFrom(z => z.AccessType))
            .ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
            .ForMember(x => x.ParentId, y => y.MapFrom(z => z.ParentId))
            .ForMember(x => x.CreatedAt, y => y.Ignore())
            .ForMember(x => x.EditedAt, y => y.Ignore())
            .ForMember(x => x.FileObjectType, y => y.Ignore())
            .ForMember(x => x.Profile, y => y.Ignore())
            .ForMember(x => x.ProfileId, y => y.Ignore())
            .ForMember(x => x.Parent, y => y.Ignore())
            .ForMember(x => x.Comments, y => y.Ignore())
            .ReverseMap();


    }
}
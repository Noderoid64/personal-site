using AutoMapper;
using PersonalSite.Api.Dtos;
using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Api.Mappings;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CommentEntity, CommentDto>()
            .ForMember(x => x.Content, y => y.MapFrom(z => z.Content))
            .ForMember(x => x.CreatedAt, y => y.MapFrom(z => z.CreatedAt))
            .ForMember(x => x.DisplayName, y => y.MapFrom(z => z.Author.Nickname))
            .ForMember(x => x.ProfilePicture, y => y.MapFrom(z => z.Author.ProfilePicture));
    }
}
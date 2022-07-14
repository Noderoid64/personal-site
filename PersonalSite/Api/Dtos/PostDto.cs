using PersonalSite.Core.Models.Entities.Enums;

namespace PersonalSite.Api.Dtos;

public class PostDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public PostAccessType AccessType { get; set; }
    public string Title { get; set; }
    public int? ParentId { get; set; }
}
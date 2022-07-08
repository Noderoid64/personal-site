using PersonalSite.Core.Entities;

namespace PersonalSite.Api.Dtos;

public class PostDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public PostAccessType PostAccessType { get; set; }
    public string Title { get; set; }
}
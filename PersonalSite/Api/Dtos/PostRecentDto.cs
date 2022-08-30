namespace PersonalSite.Api.Dtos;

public class PostRecentDto
{
    public int Id { get; set; }
    public string AuthorPicture { get; set; }
    public string AuthorNickname { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
}
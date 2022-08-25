namespace PersonalSite.Api.Dtos;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string DisplayName { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? ProfilePicture { get; set; }
}
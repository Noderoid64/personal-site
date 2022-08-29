namespace PersonalSite.Api.Dtos;

public class PostPreviewDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string Title { get; set; }
    public DateTime Created { get; set; }
    public string Author { get; set; }
}
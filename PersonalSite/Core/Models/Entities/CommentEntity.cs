namespace PersonalSite.Core.Models.Entities;

public class CommentEntity
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public int PostId { get; set; }
    public FileObjectEntity Post { get; set; }
    
    public int AuthorId { get; set; }
    public ProfileEntity Author { get; set; }
}
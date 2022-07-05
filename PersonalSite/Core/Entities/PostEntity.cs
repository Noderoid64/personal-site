using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSite.Core.Entities;

public class PostEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime EditedAt { get; set; }
    public string Content { get; set; }
    
    [ForeignKey("Profile")]
    public int ProfileId { get; set; }
    public virtual ProfileEntity Profile { get; set; }
}
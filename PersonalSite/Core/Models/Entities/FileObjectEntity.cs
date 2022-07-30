using System.ComponentModel.DataAnnotations.Schema;
using PersonalSite.Core.Models.Entities.Enums;

namespace PersonalSite.Core.Models.Entities;

public class FileObjectEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime EditedAt { get; set; }
    public string? Content { get; set; }
    public string Title { get; set; }
    
    #region flags
    public PostAccessType PostAccessType { get; set; }
    public FileObjectType FileObjectType { get; set; }

    #endregion
    
    #region Relations

    [ForeignKey("Profile")]
    public int ProfileId { get; set; }
    public virtual ProfileEntity Profile { get; set; }
    
    [ForeignKey("Parent")]
    public int? ParentId { get; set; }
    public virtual FileObjectEntity? Parent { get; set; }
    
    public virtual IList<CommentEntity> Comments { get; set; }

    #endregion
}
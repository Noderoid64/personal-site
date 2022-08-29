using System.ComponentModel.DataAnnotations.Schema;
using PersonalSite.Core.Models.Entities.Enums;

namespace PersonalSite.Core.Models.Entities;

public class FileObjectChangeEntity
{
    public int Id { get; set; }
    public ChangeType ChangeType { get; set; }
    public DateTime DateTime { get; set; }
    
    [ForeignKey("FileObjectEntity")]
    public int FileObjectEntityId { get; set; }
    public virtual FileObjectEntity FileObjectEntity { get; set; }

    public FileObjectChangeEntity()
    {
        
    }

    public FileObjectChangeEntity(int id, ChangeType changeType)
    {
        ChangeType = changeType;
        DateTime = DateTime.Now;
        FileObjectEntityId = id;
    }
}
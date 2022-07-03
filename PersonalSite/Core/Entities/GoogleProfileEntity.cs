using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSite.Core.Entities;

public class GoogleProfileEntity
{
    [Key]
    public string SourceId { get; set; }
    
    [ForeignKey("ProfileEntity")]
    public int ProfileEntityId { get; set; }
    public virtual ProfileEntity ProfileEntity { get; set; }
}
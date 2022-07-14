using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSite.Core.Models.Entities;

public class ProfileCredentialsEntity
{
    [Key]
    public int id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsConfirmed { get; set; }
    
    [ForeignKey("Profile")]
    public int UserId { get; set; }
    public virtual ProfileEntity Profile { get; set; }
}
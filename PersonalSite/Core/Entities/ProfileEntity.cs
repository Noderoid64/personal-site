using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSite.Core.Entities;

public class ProfileEntity
{
    [Key]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nickname { get; set; }
    public string? ProfilePicture { get; set; }
    
    public virtual ProfileCredentialsEntity ProfileCredentials { get; set; }
    public virtual GoogleProfileEntity? GoogleProfileEntity { get; set; }
}
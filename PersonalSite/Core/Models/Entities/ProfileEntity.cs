using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSite.Core.Models.Entities;

public class ProfileEntity
{
    [Key]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nickname { get; set; }
    public string? ProfilePicture { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpireOn { get; set; }
    
    public virtual ProfileCredentialsEntity ProfileCredentials { get; set; }
    public virtual GoogleProfileEntity? GoogleProfileEntity { get; set; }
    public virtual List<FileObjectEntity> Posts { get; set; }
}
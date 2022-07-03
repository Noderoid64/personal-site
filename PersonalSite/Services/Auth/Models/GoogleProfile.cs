namespace PersonalSite.Services.Auth.Models;

public class GoogleProfile
{
    public string SourceId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ProfilePicture { get; set; }
}
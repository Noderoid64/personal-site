namespace PersonalSite.Api.Dtos;

public class ProfileDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePicture { get; set; }
    public string NickName { get; set; }
    public string? Token { get; set; }
}
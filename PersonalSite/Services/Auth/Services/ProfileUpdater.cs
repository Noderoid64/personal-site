using PersonalSite.Core.Models.Entities;
using PersonalSite.Core.Models.Entities.Enums;
using PersonalSite.Services.Auth.Models;

namespace PersonalSite.Services.Auth.Services;

public class ProfileUpdater
{
    public ProfileEntity CreateNewOne(string nickname, string email, string password)
    {
        return new ProfileEntity()
        {
            Nickname = nickname,
            ProfileCredentials = new ProfileCredentialsEntity()
            {
                Email = email,
                Password = password
            },
            Posts = new List<FileObjectEntity>() {CreateRoot()}
        };
    }
    public ProfileEntity CreateNewOneFromGoogle(GoogleProfile gProfile)
    {
        return new ProfileEntity()
        {
            FirstName = gProfile.FirstName,
            LastName = gProfile.LastName,
            ProfilePicture = gProfile.ProfilePicture,
            GoogleProfileEntity = new GoogleProfileEntity()
            {
                SourceId = gProfile.SourceId,
            },
            Posts = new List<FileObjectEntity>() {CreateRoot()}
        };
    }

    public ProfileEntity UpdateProfile(ProfileEntity profile, GoogleProfile gProfile)
    {
        if (!string.Equals(profile.ProfilePicture, gProfile.ProfilePicture))
        {
            profile.ProfilePicture = gProfile.ProfilePicture;
            
        }

        if (string.IsNullOrEmpty(profile.Nickname))
        {
            profile.Nickname = gProfile.FirstName + " " + gProfile.LastName;
        }

        return profile;
    }

    private FileObjectEntity CreateRoot()
    {
        return new FileObjectEntity()
        {
            Title = "Root",
            FileObjectType = FileObjectType.Folder
        };
    }
}
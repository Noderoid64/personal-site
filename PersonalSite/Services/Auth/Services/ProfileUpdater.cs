using PersonalSite.Core.Entities;
using PersonalSite.Services.Auth.Models;

namespace PersonalSite.Services.Auth.Services;

public class ProfileUpdater
{
    public ProfileEntity CreateNewOne(GoogleProfile gProfile)
    {
        return new ProfileEntity()
        {
            FirstName = gProfile.FirstName,
            LastName = gProfile.LastName,
            ProfilePicture = gProfile.ProfilePicture,
            GoogleProfileEntity = new GoogleProfileEntity()
            {
                SourceId = gProfile.SourceId,
            }
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
}
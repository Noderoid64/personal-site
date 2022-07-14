using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Core.Ports;

public interface IProfileProvider
{
    public Task<ProfileEntity> GetProfileAsync(int profileId);
    public Task<ProfileEntity> GetProfileWithPostsAsync(int profileId);
}
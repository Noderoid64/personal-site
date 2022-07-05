using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Entities;
using PersonalSite.Core.Ports;

namespace PersonalSite.Infrastructure.EF.Providers;

public class ProfileProvider : _BaseProvider, IProfileProvider
{
    public async Task<ProfileEntity> GetProfileAsync(int profileId)
    {
        return await _context.Profiles
            .FirstOrDefaultAsync(x => x.Id.Equals(profileId)) ?? throw new InvalidOperationException();
    }

    public async Task<ProfileEntity> GetProfileWithPostsAsync(int profileId)
    {
        return await _context.Profiles
            .Include(x => x.Posts)
            .FirstOrDefaultAsync(x => x.Id == profileId) ?? throw new InvalidOperationException();
    }

    public ProfileProvider(ApplicationContext context) : base(context)
    {
    }
}
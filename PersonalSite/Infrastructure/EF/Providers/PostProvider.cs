using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Entities;
using PersonalSite.Core.Ports;

namespace PersonalSite.Infrastructure.EF.Providers;

public class PostProvider : _BaseProvider, IPostProvider
{
    public PostProvider(ApplicationContext context) : base(context)
    {
    }

    public async Task<PostEntity> GetPostAsync(int postId)
    {
        return await _context.Posts.FirstOrDefaultAsync(x => x.Id.Equals(postId)) ?? throw new InvalidOperationException();
    }

    public async Task<List<PostEntity>> GetPostsByProfileIdAsync(int profileId)
    {
        return await _context.Posts
            .Where(x => x.ProfileId.Equals(profileId))
            .ToListAsync();
    }

    public void SavePost(PostEntity post)
    {
        _context.Posts.Add(post);
    }

    
}
using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Models.Entities;
using PersonalSite.Core.Ports;

namespace PersonalSite.Infrastructure.EF.Providers;

public class PostProvider : _BaseProvider, IPostProvider
{
    public PostProvider(ApplicationContext context) : base(context)
    {
    }

    public async Task<FileObjectEntity> GetFileObjectAsync(int postId)
    {
        return await _context.Posts.FirstAsync(x => x.Id.Equals(postId));
    }

    public async Task<FileObjectEntity> GetFileObjectRootAsync(int profileId)
    {
        return await _context.Posts.FirstAsync(x => x.ProfileId == profileId && x.ParentId == null);
    }

    public async Task<List<FileObjectEntity>> GetPostsByProfileIdAsync(int profileId)
    {
        return await _context.Posts
            .Where(x => x.ProfileId.Equals(profileId))
            .ToListAsync();
    }

    public void SaveFileObject(FileObjectEntity fileObject)
    {
        _context.Posts.Add(fileObject);
    }

    public async Task DeleteFileObjectAsync(FileObjectEntity fileObject)
    {
        _context.Posts.Remove(fileObject);
    }

    
}
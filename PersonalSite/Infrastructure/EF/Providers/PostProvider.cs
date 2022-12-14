using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Models.Entities;
using PersonalSite.Core.Models.Entities.Enums;
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

    public async Task<List<FileObjectEntity>> GetRecentPosts()
    {
        return await _context.Posts
            .OrderBy(x => x.CreatedAt)
            .Include(x => x.Profile)
            .Where(x => FileObjectType.Post == x.FileObjectType && PostAccessType.Public == x.PostAccessType)
            .Take(10)
            .ToListAsync();
    }

    public async Task<FileObjectEntity> GetPostWithCommentsAsync(int postId)
    {
        return await _context.Posts
            .Include(x => x.Comments)
            .ThenInclude(x => x.Author)
            .FirstAsync(x => x.Id == postId);
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
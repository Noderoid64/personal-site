using PersonalSite.Core.Entities;

namespace PersonalSite.Core.Ports;

public interface IPostProvider
{
    public Task<PostEntity> GetPostAsync(int postId);
    public Task<List<PostEntity>> GetPostsByProfileIdAsync(int profileId);
    public void SavePost(PostEntity post);
    public Task SaveAsync();
}
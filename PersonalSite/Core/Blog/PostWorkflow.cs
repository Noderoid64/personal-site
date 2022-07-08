using PersonalSite.Core.Blog.Services;
using PersonalSite.Core.Entities;
using PersonalSite.Core.Ports;
using PersonalSite.Infrastructure.Common.Models;

namespace PersonalSite.Core.Blog;

public class PostWorkflow
{
    private readonly IPostProvider _postProvider;
    private readonly IProfileProvider _profileProvider;

    private readonly PostMerger _postMerger;

    public PostWorkflow(IPostProvider postProvider, IProfileProvider profileProvider, PostMerger postMerger)
    {
        _postProvider = postProvider;
        _profileProvider = profileProvider;
        _postMerger = postMerger;
    }

    public async Task<Result<PostEntity>> GetPostAsync(int userId, int postId)
    {
        var user = await _profileProvider.GetProfileWithPostsAsync(userId);
        ArgumentNullException.ThrowIfNull(user);
        
        var post = user.Posts.FirstOrDefault(x => x.Id == postId);
        if (post == null)
            return Result<PostEntity>.Fail($"This user does not have post with id: {postId}");
        return Result<PostEntity>.Success(post);
    }
    
    public Task<List<PostEntity>> GetUserPostsAsync(int userId)
    {
        return _postProvider.GetPostsByProfileIdAsync(userId);
    }

    public async Task SavePost(int profileId, PostEntity post)
    {
        var profile = await _profileProvider.GetProfileAsync(profileId);
        
        ArgumentNullException.ThrowIfNull(profile);
        
        if (post.Id != default)
        {
            var existed = await _postProvider.GetPostAsync(post.Id);
            ArgumentNullException.ThrowIfNull(existed);
            _postMerger.Merge(existed, post);
        }
        else
        {
            var postToSave = new PostEntity()
            {
                Content = post.Content,
                ProfileId = profile.Id,
                CreatedAt = DateTime.UtcNow,
                EditedAt = DateTime.UtcNow,
                Title = post.Title,
                PostAccessType = post.PostAccessType
            };
            _postProvider.SavePost(postToSave);
        }
        
        await _postProvider.SaveAsync();
    }
}
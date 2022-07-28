using PersonalSite.Core.Blog.Models;
using PersonalSite.Core.Blog.Services;
using PersonalSite.Core.Models.Entities;
using PersonalSite.Core.Models.Entities.Enums;
using PersonalSite.Core.Ports;
using PersonalSite.Infrastructure.Common.Models;

namespace PersonalSite.Core.Blog;

public class PostWorkflow
{
    private readonly IPostProvider _postProvider;
    private readonly IProfileProvider _profileProvider;
    private readonly FileObjectBuilder _fileObjectBuilder;

    private readonly PostMerger _postMerger;

    public PostWorkflow(IPostProvider postProvider, IProfileProvider profileProvider, PostMerger postMerger, FileObjectBuilder fileObjectBuilder)
    {
        _postProvider = postProvider;
        _profileProvider = profileProvider;
        _postMerger = postMerger;
        _fileObjectBuilder = fileObjectBuilder;
    }

    public async Task<Result<FileObjectEntity>> GetPostAsync(int userId, int postId)
    {
        var user = await _profileProvider.GetProfileWithPostsAsync(userId);
        ArgumentNullException.ThrowIfNull(user);
        
        var post = user.Posts.FirstOrDefault(x => x.Id == postId);
        if (post == null)
            return Result<FileObjectEntity>.Fail($"This user does not have post with id: {postId}");
        return Result<FileObjectEntity>.Success(post);
    }
    
    public async Task<FileObject> GetUserPostsAsync(int userId)
    {
        var entities = await _postProvider.GetPostsByProfileIdAsync(userId);
        return _fileObjectBuilder.Build(entities);
    }

    public async Task SavePost(int profileId, FileObjectEntity fileObject)
    {
        var profile = await _profileProvider.GetProfileAsync(profileId);
        
        if (fileObject.IsNew())
        {
            int parentId = await FindParent(fileObject, profileId);
            var postToSave = CreateNewPost(fileObject, profile.Id, parentId);
            _postProvider.SaveFileObject(postToSave);
        }
        else
        {
            var existed = await _postProvider.GetFileObjectAsync(fileObject.Id);
            _postMerger.Merge(existed, fileObject);
        }
        
        await _postProvider.SaveAsync();
    }

    public async Task<Result<int>> SaveFolderAsync(int profileId, string title, int parentId)
    {
        var profile = await _profileProvider.GetProfileAsync(profileId);
        var folder = CreateNewFolder(profile.Id, title, parentId);
        _postProvider.SaveFileObject(folder);
        await _postProvider.SaveAsync();
        return Result<int>.Success(folder.Id);
    }

    public async Task DeleteFileAsync(int profileId, int fileId)
    {
        var profile = await _profileProvider.GetProfileAsync(profileId);
        var postToDelete = await _postProvider.GetFileObjectAsync(fileId);
        if (profile.Id != profileId)
            return;
        await _postProvider.DeleteFileObjectAsync(postToDelete);
        await _postProvider.SaveAsync();
    }

    private FileObjectEntity CreateNewFolder(int profileId, string title, int parentId)
    {
        return new FileObjectEntity()
        {
            Title = title,
            ParentId = parentId,
            ProfileId = profileId,
            CreatedAt = DateTime.UtcNow,
            EditedAt = DateTime.UtcNow,
            FileObjectType = FileObjectType.Folder
        };
    }

    private async Task<int> FindParent(FileObjectEntity fileObject, int profileId)
    {
        return fileObject.HasNoParent()
            ? (await _postProvider.GetFileObjectRootAsync(profileId)).Id
            : fileObject.ParentId.Value;
    }

    private FileObjectEntity CreateNewPost(FileObjectEntity fileObject, int profileId, int parentId)
    {
        return new FileObjectEntity()
        {
            Content = fileObject.Content,
            ProfileId = profileId,
            CreatedAt = DateTime.UtcNow,
            EditedAt = DateTime.UtcNow,
            Title = fileObject.Title,
            PostAccessType = fileObject.PostAccessType,
            ParentId = parentId
        };
    }
}
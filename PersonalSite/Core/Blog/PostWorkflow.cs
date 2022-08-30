using PersonalSite.Api.Dtos;
using PersonalSite.Core.Blog.Models;
using PersonalSite.Core.Blog.Services;
using PersonalSite.Core.Models.Entities;
using PersonalSite.Core.Models.Entities.Enums;
using PersonalSite.Core.Ports;
using PersonalSite.Infrastructure.Common.Models;
using PersonalSite.Services.FullTextSearch;

namespace PersonalSite.Core.Blog;

public class PostWorkflow
{
    private readonly IPostProvider _postProvider;
    private readonly IProfileProvider _profileProvider;
    private readonly FileObjectBuilder _fileObjectBuilder;
    private readonly IPostChangesGateway _postChangesGateway;

    private readonly PostMerger _postMerger;

    public PostWorkflow(
        IPostProvider postProvider, 
        IProfileProvider profileProvider, 
        PostMerger postMerger, 
        FileObjectBuilder fileObjectBuilder,
        IPostChangesGateway postChangesGateway)
    {
        _postProvider = postProvider;
        _profileProvider = profileProvider;
        _postMerger = postMerger;
        _fileObjectBuilder = fileObjectBuilder;
        _postChangesGateway = postChangesGateway;
    }

    public async Task<Result<FileObjectEntity>> GetPostAsync(int postId)
    {
        var post = await _postProvider.GetPostWithCommentsAsync(postId);
        return Result<FileObjectEntity>.Success(post);
    }
    
    public async Task<FileObjectTree> GetUserPostTreeAsync(int userId)
    {
        var entities = await _postProvider.GetPostsByProfileIdAsync(userId);
        return _fileObjectBuilder.Build(entities);
    }

    public async Task<IEnumerable<PostPreviewDto>> FindPosts(string searchString)
    {
        var serv = TextSearchIndex.GetInstance();
        return await serv.FindPosts(searchString);
    }

    public async Task SavePost(int profileId, FileObjectEntity fileObject)
    {
        var profile = await _profileProvider.GetProfileAsync(profileId);
        
        if (fileObject.IsNew())
        {
            int parentId = await FindParent(fileObject, profileId);
            var postToSave = _fileObjectBuilder.CreatePost(fileObject, profile.Id, parentId);
            _postProvider.SaveFileObject(postToSave);
            await _postProvider.SaveAsync();
            await _postChangesGateway.PostAdded(postToSave.Id);
        }
        else
        {
            var existed = await _postProvider.GetFileObjectAsync(fileObject.Id);
            _postMerger.Merge(existed, fileObject);
            await _postProvider.SaveAsync();
            await _postChangesGateway.PostChanged(existed.Id);
        }
        
    }

    public async Task<Result<int>> SaveFolderAsync(int profileId, string title, int parentId)
    {
        var profile = await _profileProvider.GetProfileAsync(profileId);
        var folder = _fileObjectBuilder.CreateFolder(profile.Id, title, parentId);
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

        if (postToDelete.FileObjectType == FileObjectType.Post)
        {
            await _postChangesGateway.PostDeleted(postToDelete.Id);
        }
    }

    public async Task<IEnumerable<FileObjectEntity>> GetRecentPosts()
    {
        return await _postProvider.GetRecentPosts();
    }
    

    // TODO: Move to separate file
    private async Task<int> FindParent(FileObjectEntity fileObject, int profileId)
    {
        return fileObject.HasNoParent()
            ? (await _postProvider.GetFileObjectRootAsync(profileId)).Id
            : fileObject.ParentId.Value;
    }
}
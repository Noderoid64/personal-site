using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Core.Ports;

public interface IPostProvider
{
    public Task<FileObjectEntity> GetFileObjectAsync(int postId);
    public Task<FileObjectEntity> GetFileObjectRootAsync(int profileId);
    public Task<List<FileObjectEntity>> GetPostsByProfileIdAsync(int profileId);
    public Task<FileObjectEntity> GetPostWithCommentsAsync(int postId);
    public void SaveFileObject(FileObjectEntity fileObject);
    public Task DeleteFileObjectAsync(FileObjectEntity fileObjectEntity);
    public Task SaveAsync();
}
using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Core.Ports;

public interface IPostProvider
{
    public Task<FileObjectEntity> GetFileObjectAsync(int postId);
    public Task<List<FileObjectEntity>> GetPostsByProfileIdAsync(int profileId);
    public void SaveFileObject(FileObjectEntity fileObject);
    public Task DeleteFileObjectAsync(FileObjectEntity fileObjectEntity);
    public Task SaveAsync();
}
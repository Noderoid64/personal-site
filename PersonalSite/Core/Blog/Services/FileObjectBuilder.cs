using PersonalSite.Core.Blog.Models;
using PersonalSite.Core.Models.Entities;
using PersonalSite.Core.Models.Entities.Enums;

namespace PersonalSite.Core.Blog.Services;

public class FileObjectBuilder
{
    public FileObjectEntity CreateFolder(int profileId, string title, int parentId)
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

    public FileObjectEntity CreatePost(FileObjectEntity fileObject, int profileId, int parentId)
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
    
    public FileObjectTree Build(IEnumerable<FileObjectEntity> entities)
    {
        var root = entities
            .Where(x => x.ParentId == null)
            .Select(x => new FileObjectTree(x))
            .Single();

        FillChildren(root, entities);
        
        return root;
    }

    private void FillChildren(FileObjectTree parent, IEnumerable<FileObjectEntity> entities)
    {
        parent.Children = entities
            .Where(x => x.ParentId == parent.Id)
            .Select(x => new FileObjectTree(x))
            .ToList();
        
        foreach (var item in parent.Children)
        {
            FillChildren(item, entities);
        }
    }
}
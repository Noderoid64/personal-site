using PersonalSite.Core.Models.Entities;
using PersonalSite.Core.Models.Entities.Enums;

namespace PersonalSite.Core.Blog.Models;

public class FileObjectTree
{
    public int Id { get; set; }
    public bool IsFolder { get; set; }
    public string Title { get; set; }
    public int ParentId { get; set; }
    public List<FileObjectTree> Children { get; set; }
    
    public FileObjectTree(FileObjectEntity entity)
    {
        Id = entity.Id;
        IsFolder = entity.FileObjectType == FileObjectType.Folder;
        Title = entity.Title;
        Children = new List<FileObjectTree>();
        ParentId = entity.ParentId ?? default;
    }
}
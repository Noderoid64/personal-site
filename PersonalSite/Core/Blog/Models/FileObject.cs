using PersonalSite.Core.Models.Entities;
using PersonalSite.Core.Models.Entities.Enums;

namespace PersonalSite.Core.Blog.Models;

public class FileObject
{
    public int Id { get; set; }
    public bool IsFolder { get; set; }
    public string Title { get; set; }
    public FileObject? Parent { get; set; }
    public List<FileObject> Children { get; set; }
    
    public FileObject(FileObjectEntity entity)
    {
        Id = entity.Id;
        IsFolder = entity.FileObjectType == FileObjectType.Folder;
        Title = entity.Title;
        Children = new List<FileObject>();
    }
}
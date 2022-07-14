using PersonalSite.Core.Blog.Models;
using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Core.Blog.Services;

public class FileObjectBuilder
{
    public FileObject Build(IEnumerable<FileObjectEntity> entities)
    {
        var root = entities
            .Where(x => x.ParentId == null)
            .Select(x => new FileObject(x))
            .Single();

        FillChildren(root, entities);
        
        return root;
    }

    private void FillChildren(FileObject parent, IEnumerable<FileObjectEntity> entities)
    {
        parent.Children = entities
            .Where(x => x.ParentId == parent.Id)
            .Select(x => new FileObject(x))
            .ToList();
        
        foreach (var item in parent.Children)
        {
            FillChildren(item, entities);
        }
    }
}
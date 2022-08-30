using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Core.Blog.Services;

public class PostMerger
{
    public void Merge(FileObjectEntity oldOne, FileObjectEntity newOne)
    {
        ArgumentNullException.ThrowIfNull(oldOne);
        ArgumentNullException.ThrowIfNull(newOne);

        oldOne.Content = newOne.Content;
        oldOne.EditedAt = DateTime.Now;
        oldOne.Title = newOne.Title;
        oldOne.PostAccessType = newOne.PostAccessType;
    }
}
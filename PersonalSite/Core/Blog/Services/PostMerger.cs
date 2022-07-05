using PersonalSite.Core.Entities;

namespace PersonalSite.Core.Blog.Services;

public class PostMerger
{
    public PostEntity Merge(PostEntity oldOne, PostEntity newOne)
    {
        ArgumentNullException.ThrowIfNull(oldOne);
        ArgumentNullException.ThrowIfNull(newOne);

        oldOne.Content = newOne.Content;
        oldOne.EditedAt = DateTime.Now;
        
        return oldOne;
    }
}
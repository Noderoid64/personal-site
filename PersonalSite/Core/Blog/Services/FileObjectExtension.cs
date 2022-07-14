using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Core.Blog.Services;

public static class FileObjectExtension
{
    public static bool IsNew(this FileObjectEntity fo)
    {
        return fo.Id == default;
    }

    public static bool HasNoParent(this FileObjectEntity fo)
    {
        return fo.ParentId == default;
    }
}
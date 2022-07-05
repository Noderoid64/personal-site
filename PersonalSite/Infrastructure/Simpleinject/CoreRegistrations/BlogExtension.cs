using PersonalSite.Core.Blog;
using PersonalSite.Core.Blog.Services;
using SimpleInjector;

namespace PersonalSite.Infrastructure.SimpleInject.CoreRegistrations;

public static class BlogExtension
{
    public static void RegisterCoreBlog(this Container container)
    {
        container.Register<PostMerger>();
        container.Register<PostWorkflow>();
    }
}
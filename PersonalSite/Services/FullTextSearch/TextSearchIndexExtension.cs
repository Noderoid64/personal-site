using PersonalSite.Core.Ports;
using SimpleInjector;

namespace PersonalSite.Services.FullTextSearch;

public static class TextSearchIndexExtension
{
    public static void RegisterTextSearchIndex(this Container container)
    {
        container.Register<IPostChangesGateway, PostChangesGateway>();
    }
}
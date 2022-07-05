using PersonalSite.Core.Ports;
using PersonalSite.Infrastructure.EF.Providers;
using SimpleInjector;

namespace PersonalSite.Infrastructure.EF;

public static class EntityFrameworkExtensions
{
    public static void RegisterDbContext(this Container container)
    {
        container.Register<ApplicationContext, ApplicationContext>(Lifestyle.Scoped);
        container.Register<IPostProvider, PostProvider>();
        container.Register<IProfileProvider, ProfileProvider>();
    }
}
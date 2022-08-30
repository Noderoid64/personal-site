using System.Diagnostics;
using PersonalSite.Core.Ports;
using PersonalSite.Infrastructure.EF.Providers;
using Serilog;
using SimpleInjector;

namespace PersonalSite.Infrastructure.EF;

public static class EntityFrameworkExtensions
{
    public static void RegisterDbContext(this Container container)
    {
        container.Register<ApplicationContext, ApplicationContext>(Lifestyle.Scoped);
        container.Register<IPostProvider, PostProvider>();
        container.Register<IProfileProvider, ProfileProvider>();
        container.Register<ICommentProvider, CommentProvider>();
    }

    public static void AddEF(this WebApplicationBuilder webApplicationBuilder)
    {
        var sw = new Stopwatch();
        sw.Start();
        try
        {
            using (var context = new ApplicationContext(webApplicationBuilder.Configuration))
            {
                var isExisted = context.Database.EnsureCreated();
                sw.Stop();
                Log.Information("Database ({Status}) has been initialized in {Elapsed:000}ms.", isExisted ? "Created" : "Already exist", sw.ElapsedMilliseconds);
            }
        }
        catch (Exception e)
        {
            Log.Fatal("Database initialization failed: {Exception}", e);
            throw;
        }
        
    }
}
using PersonalSite.Infrastructure.Automapper;
using PersonalSite.Infrastructure.EF;
using PersonalSite.Infrastructure.SimpleInject.CoreRegistrations;
using PersonalSite.Services.Auth;
using PersonalSite.Services.FullTextSearch;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace PersonalSite.Infrastructure.SimpleInject;

public static class SimpleInjectExtension
{
    public static void AddSimpleInjectorDi(this WebApplicationBuilder wab, Container container)  
    {        
        container.Options.DefaultLifestyle = Lifestyle.Scoped;  
        container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
        container.Options.ConstructorResolutionBehavior = new ConstructorResolutionBehavior();

        // Here should be registrations
        container.RegisterAutomapper();
        container.RegisterAuth(wab.Configuration);
        container.RegisterTextSearchIndex();
        container.RegisterDbContext();
        container.RegisterCoreBlog();
  
        wab.Services.AddSimpleInjector(container, op =>  
        {  
            op.AddAspNetCore().AddControllerActivation();  
        });
    }

    public static void AddSimpleInjectorDi(this IServiceProvider sp, Container container)
    {
        sp.UseSimpleInjector(container);
        container.Verify();
    }
}
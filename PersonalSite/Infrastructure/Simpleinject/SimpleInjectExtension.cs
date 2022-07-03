using PersonalSite.Infrastructure.EF;
using PersonalSite.Services.Auth;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace PersonalSite.Infrastructure.SimpleInject;

public static class SimpleInjectExtension
{
    public static void AddSimpleInjectorDi(this WebApplicationBuilder wab, Container container)  
    {        
        container.Options.DefaultLifestyle = Lifestyle.Scoped;  
        container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();  

        // Here should be registrations
        container.RegisterAuth(wab.Configuration);
        container.RegisterDbContext();
  
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
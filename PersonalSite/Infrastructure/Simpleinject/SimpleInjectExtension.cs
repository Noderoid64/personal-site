using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace PersonalSite.Infrastructure.SimpleInject;

public static class SimpleInjectExtension
{
    public static void AddSimpleInjectorContainer(this IServiceCollection sc, Container container)  
    {        
        container.Options.DefaultLifestyle = Lifestyle.Scoped;  
        container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();  

        // Here should be registrations
        // container.Register<ILogger, ConsoleLogger>();  
  
        sc.AddSimpleInjector(container, op =>  
        {  
            op.AddAspNetCore().AddControllerActivation();  
        });
        
        
  
    }
}
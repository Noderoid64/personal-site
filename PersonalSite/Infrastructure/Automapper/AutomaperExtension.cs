using AutoMapper;
using SimpleInjector;

namespace PersonalSite.Infrastructure.Automapper;

public static class AutomaperExtension
{
    public static void RegisterAutomapper(this Container container)  
    {  
        // Create config that scan current Assembly on existing Profiles
        var config = new MapperConfiguration(cfg =>  
            cfg.AddMaps(new[]  
            {  
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name  
            }));

        // Check that mapping is without non mapped fields
        config.AssertConfigurationIsValid();  
  
        var mapper = config.CreateMapper();

        // Register in the DI system
        container.Register(() => mapper);  
    }
}
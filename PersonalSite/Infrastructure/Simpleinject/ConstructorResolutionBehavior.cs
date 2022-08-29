using System.Reflection;
using SimpleInjector.Advanced;

namespace PersonalSite.Infrastructure.SimpleInject;

public class ConstructorResolutionBehavior : IConstructorResolutionBehavior
{
    public ConstructorInfo? TryGetConstructor(Type implementationType, out string? errorMessage)
    {
        errorMessage = $"{implementationType} has no public constructors.";
        
        return (
                from ctor in implementationType.GetConstructors()
                orderby ctor.GetParameters().Length descending
                select ctor)
            .FirstOrDefault();
    }
}
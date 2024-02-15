using Common.AspCore.Attributes;
using System.Reflection;

namespace Common.ANCore.Extensions;
internal static class IServiceProviderExtension
{
    public static void UseAutowired(this IServiceProvider serviceProvider)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                         .Where(prop => Attribute.IsDefined(prop, typeof(AutowiredAttribute))))
                {
                    var service = serviceProvider.GetService(type);
                    if (service == null)
                    {
                        foreach (var item in type.GetInterfaces())
                        {
                            if (service != null)
                                break;

                            service = serviceProvider.GetService(item);
                        }

                        if (service == null)
                            continue;
                    }

                    property.SetValue(service, serviceProvider.GetService(property.PropertyType));
                }
            }
        }
    }
}
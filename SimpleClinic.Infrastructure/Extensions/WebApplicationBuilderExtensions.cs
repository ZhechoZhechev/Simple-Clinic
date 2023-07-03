using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SimpleClinic.Infrastructure.Extensions;
/// <summary>
/// Builder Extensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Extension that adds all new service VIA reflection
    /// </summary>
    /// <param name="services">collection of service</param>
    /// <param name="serviceType">type service to add</param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void AddApplicationServices(this IServiceCollection services, Type serviceType) 
    {
        Assembly? sericeAssembly = Assembly.GetAssembly(serviceType);
        if (sericeAssembly == null)
        {
            throw new InvalidOperationException("Invalid service type provided");
        }

        Type[] serviceTypes = sericeAssembly
            .GetTypes()
            .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
            .ToArray();

        foreach (var implementationType in serviceTypes)
        {
            Type? interfaceType = implementationType
                .GetInterface($"!{implementationType.Name}");

            if (interfaceType == null)
            {
                throw new InvalidOperationException($"No interface is provided with service with name {implementationType.Name}");
            }

            services.AddScoped(interfaceType, implementationType);
        }
    }
}

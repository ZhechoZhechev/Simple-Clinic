﻿using Microsoft.Extensions.DependencyInjection;
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
        Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
        if (serviceAssembly == null)
        {
            throw new InvalidOperationException("Invalid service type provided!");
        }

        Type[] implementationTypes = serviceAssembly
            .GetTypes()
            .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
            .ToArray();
        foreach (Type implementationType in implementationTypes)
        {
            Type? interfaceType = implementationType
                .GetInterface($"I{implementationType.Name}");
            if (interfaceType == null)
            {
                throw new InvalidOperationException(
                    $"No interface is provided for the service with name: {implementationType.Name}");
            }

            services.AddScoped(interfaceType, implementationType);
        }
    }
}

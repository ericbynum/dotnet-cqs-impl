using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Cqs
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCqs(this IServiceCollection services)
        {
            services.AddScoped<ICqsResolver>(provider => new CqsResolver(
                getServiceByTypeFromContainer: type => provider.GetService(type)));

            RegisterCqsTypesInAssembly(services, typeof(ICommand));
            RegisterCqsTypesInAssembly(services, typeof(IQuery));

            return services;
        }
        
        private static void RegisterCqsTypesInAssembly(IServiceCollection services, Type interfaceType)
        {
            Dictionary<Type, Type> cqsTypes = interfaceType.Assembly.GetTypes()
                .Where(type => type.IsClass && type.IsAssignableTo(interfaceType))
                .ToDictionary(type => type.GetInterfaces().First());

            foreach ((Type serviceType, Type implementationType) in cqsTypes)
            {
                services.AddScoped(serviceType, implementationType);
            }
        }
    }
}
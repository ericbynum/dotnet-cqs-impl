using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Cqs
{
    public static class ServiceClassExtensions
    {
        public static IServiceCollection AddCqs(this IServiceCollection services)
        {
            services.AddScoped<ICqsResolver>(provider => new CqsResolver(
                getServiceByTypeFromContainer: type => provider.GetRequiredService(type)));

            RegisterCommandsAndQueriesInAssembly(services);

            return services;
        }

        private static void RegisterCommandsAndQueriesInAssembly(IServiceCollection services)
        {
            IEnumerable<Type> cqsTypes = typeof(ICqsResolver).Assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && (
                    i.GetGenericTypeDefinition() == typeof(ICommand<>) || 
                    i.GetGenericTypeDefinition() == typeof(IQuery<>))));

            foreach (Type cqsType in cqsTypes)
            {
                services.AddScoped(cqsType);
            }
        }
    }
}
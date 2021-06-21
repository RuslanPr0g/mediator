using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediatorFromScratch.DependencyInjection
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddMediator(
            this IServiceCollection services,
            ServiceLifetime lifetime,
            params Type[] markers)
        {
            var handler = new Dictionary<Type, Type>();

            foreach (var marker in markers)
            {
                var assembly = marker.Assembly;
                var requests = GetClasses(assembly, typeof(IRequest<>));
                var handlers = GetClasses(assembly, typeof(IHandler<,>));

                requests.ForEach(x =>
                {
                    handler[x] = handlers.SingleOrDefault(y => x == y.GetInterface("IHandler`2")!.GetGenericArguments()[0]);
                });

                var serviceDescriptor = handlers.Select(x => new ServiceDescriptor(x, x, lifetime));
                services.TryAdd(serviceDescriptor);
            }

            services.AddSingleton<IMediator>(x => new Mediator(x.GetRequiredService, handler));

            return services;
        }

        private static List<Type> GetClasses(Assembly assembly, Type typeToMatch)
        {
            return assembly.ExportedTypes
                   .Where(type =>
                   {
                       var genericInterfaceTypes = type.GetInterfaces().Where(x => x.IsGenericType).ToList();
                       var implementRequestType = genericInterfaceTypes
                           .Any(x => x.GetGenericTypeDefinition() == typeToMatch);
                       return !type.IsInterface && !type.IsAbstract && implementRequestType;
                   }).ToList();
        }
    }
}

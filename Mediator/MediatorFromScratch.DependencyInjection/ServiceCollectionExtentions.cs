using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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


            return services;
        }
    }
}

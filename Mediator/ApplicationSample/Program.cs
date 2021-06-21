using MediatorFromScratch;
using MediatorFromScratch.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddMediator(ServiceLifetime.Scoped, typeof(Program))
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var request = new PrintToConsoleRequest()
            {
                Text = "Hello Mediator!"
            };

            await mediator.SendAsync(request);

            // ** The flow:
            // request
            // handler
            // mediator
            // request => mediator => handler => response
        }
    }
}

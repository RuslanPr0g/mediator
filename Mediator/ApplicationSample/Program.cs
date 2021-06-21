using MediatorFromScratch;
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
                .AddTransient<PrintToConsoleHandler>()
                .BuildServiceProvider();

            var handlerDetails = new Dictionary<Type, Type>()
            {
                { typeof(PrintToConsoleRequest), typeof(PrintToConsoleHandler) }
            };

            var request = new PrintToConsoleRequest()
            {
                Text = "Hello Mediator!"
            };

            IMediator mediator = new Mediator(serviceProvider.GetRequiredService, handlerDetails);

            await mediator.SendAsync(request);

            // ** The flow:
            // request
            // handler
            // mediator
            // request => mediator => handler => response
        }
    }
}

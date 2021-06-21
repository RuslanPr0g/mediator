using MediatorFromScratch;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApplicationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<PrintToConsoleHandler>()
                .BuildServiceProvider();

            var request = new PrintToConsoleRequest()
            {
                Text = "Hello Mediator!"
            };

            IMediator mediator = new Mediator(serviceProvider.GetRequiredService);

            // ** The flow:
            // request
            // handler
            // mediator
            // request => mediator => handler => response
        }
    }
}

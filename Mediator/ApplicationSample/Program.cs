using System;

namespace ApplicationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var request = new PrintToConsoleRequest()
            {
                Text = "Hello Mediator!"
            };

            // request
            // handler
            // mediator
            // request => mediator => handler => response
        }
    }
}

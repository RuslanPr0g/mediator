using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorFromScratch
{
    public class Mediator : IMediator
    {
        private readonly Func<Type, object> _serviceResolver;

        public Mediator(Func<Type, object> serviceResolver)
        {
            _serviceResolver = serviceResolver;
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {

        }
    }
}

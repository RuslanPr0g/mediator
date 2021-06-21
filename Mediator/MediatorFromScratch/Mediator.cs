using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorFromScratch
{
    public class Mediator : IMediator
    {
        private readonly Func<Type, object> _serviceResolver;
        private readonly IDictionary<Type, Type> _handlerDetails;

        public Mediator(Func<Type, object> serviceResolver,
            IDictionary<Type, Type> handlerDetails)
        {
            _serviceResolver = serviceResolver;
            _handlerDetails = handlerDetails;
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();

            if (!_handlerDetails.ContainsKey(requestType))
            {
                throw new Exception($"No handler to handle this request of type {requestType}");
            }

            _handlerDetails.TryGetValue(requestType, out var requestHandlerType);
            var handler = _serviceResolver(requestHandlerType);

            return await (Task<TResponse>)handler.GetType().GetMethod("HandleAsync")
                .Invoke(handler, new[] { request });
        }
    }
}

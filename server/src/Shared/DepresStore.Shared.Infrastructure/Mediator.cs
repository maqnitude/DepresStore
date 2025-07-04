using DepresStore.Shared.Kernel.Mediator;

namespace DepresStore.Shared.Infrastructure
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResponse> SendAsync<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            dynamic handler = _serviceProvider.GetService(handlerType)
                ?? throw new InvalidOperationException($"Handler for {request.GetType()} not found");

            return handler.HandleAsync((dynamic)request, cancellationToken);
        }

        public Task SendAsync<TRequest>(
            TRequest request,
            CancellationToken cancellationToken = default) where TRequest : IRequest
        {
            ArgumentNullException.ThrowIfNull(request);

            var handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
            dynamic handler = _serviceProvider.GetService(handlerType)
                ?? throw new InvalidOperationException($"Handler for {request.GetType()} not found");

            return handler.HandleAsync((dynamic)request, cancellationToken);
        }
    }
}
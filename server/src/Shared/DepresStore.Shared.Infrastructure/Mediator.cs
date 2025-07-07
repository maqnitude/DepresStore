using DepresStore.Shared.Kernel.Cqrs;
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

        public Task<TResponse> SendAsync<TRequest, TResponse>(
            TRequest request,
            CancellationToken cancellationToken = default)
            where TRequest : IRequest<TResponse>
        {
            ArgumentNullException.ThrowIfNull(request);

            var handler = ResolveHandler(
                [
                    typeof(IQueryHandler<,>),
                    typeof(ICommandHandler<,>),
                    typeof(IRequestHandler<,>)
                ],
                [
                    request.GetType(),
                    typeof(TResponse)
                ]
            ) ?? throw new InvalidOperationException($"Handler for {request.GetType()} not found");

            return handler.HandleAsync((dynamic)request, cancellationToken);
        }

        public Task SendAsync<TRequest>(
            TRequest request,
            CancellationToken cancellationToken = default)
            where TRequest : IRequest
        {
            ArgumentNullException.ThrowIfNull(request);

            var handler = ResolveHandler(
                [
                    typeof(ICommandHandler<>),
                    typeof(IRequestHandler<>)
                ],
                [
                    request.GetType()
                ]
            ) ?? throw new InvalidOperationException($"Handler for {request.GetType()} not found");

            return handler.HandleAsync((dynamic)request, cancellationToken);
        }

        private dynamic? ResolveHandler(Type[] handlerTypeDefinitions, Type[] handlerTypeArguments)
        {
            foreach (var handlerTypeDef in handlerTypeDefinitions)
            {
                var handlerType = handlerTypeDef.MakeGenericType(handlerTypeArguments);
                dynamic? handler = _serviceProvider.GetService(handlerType);

                if (handler != null)
                {
                    return handler;
                }
            }

            return null;
        }
    }
}
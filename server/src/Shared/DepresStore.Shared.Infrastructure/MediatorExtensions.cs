using DepresStore.Shared.Kernel.Application;

namespace DepresStore.Shared.Infrastructure
{
    public static class MediatorExtensions
    {
        /// <summary>
        /// Wraps <see cref="Mediator.SendAsync{TRequest, TResponse}(TRequest, CancellationToken)"/> for convenience.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="mediator"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<TResponse> SendAsync<TResponse>(
            this IMediator mediator,
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            return mediator.SendAsync<IRequest<TResponse>, TResponse>(request, cancellationToken);
        }
    }
}
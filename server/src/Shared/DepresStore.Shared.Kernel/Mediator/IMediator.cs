namespace DepresStore.Shared.Kernel.Mediator
{
    public interface IMediator
    {
        /// <summary>
        /// Asynchronously sends a request to a single handler.
        /// </summary>
        /// <typeparam name="TResponse">Response type</typeparam>
        /// <param name="request">Request with a response.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A TResponse which is returned by the handler.</returns>
        Task<TResponse> SendAsync<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously sends a request to a single handler with no response.
        /// </summary>
        /// <typeparam name="TRequest">Request type.</typeparam>
        /// <param name="request">Request with no response.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task SendAsync<TRequest>(
            TRequest request,
            CancellationToken cancellationToken = default) where TRequest : IRequest;
    }
}
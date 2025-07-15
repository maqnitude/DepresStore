namespace DepresStore.Shared.Kernel.Application
{
    public interface IMediator
    {
        /// <summary>
        /// Asynchronously sends a request to a single handler.
        /// </summary>
        /// <typeparam name="TRequest">Request type.</typeparam>
        /// <typeparam name="TResponse">Response type.</typeparam>
        /// <param name="request">Request with a response.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A task representing the asynchronous operation,
        /// with a result of type <typeparamref name="TResponse"/>.
        /// </returns>
        Task<TResponse> SendAsync<TRequest, TResponse>(
            TRequest request,
            CancellationToken cancellationToken = default)
            where TRequest : IRequest<TResponse>;

        /// <summary>
        /// Asynchronously sends a request to a single handler with no response.
        /// </summary>
        /// <typeparam name="TRequest">Request type.</typeparam>
        /// <param name="request">Request with no response.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        Task SendAsync<TRequest>(
            TRequest request,
            CancellationToken cancellationToken = default)
            where TRequest : IRequest;
    }
}
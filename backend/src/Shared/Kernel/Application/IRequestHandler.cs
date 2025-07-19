namespace DepresStore.Shared.Kernel.Application
{
    /// <summary>
    /// Defines a handler for a request with a response.
    /// </summary>
    /// <typeparam name="TRequest">Request type.</typeparam>
    /// <typeparam name="TResponse">Response type.</typeparam>
    public interface IRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Handles the request sent by the mediator.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response from the request.</returns>
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Defines a handler for a request with no response.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IRequestHandler<in TRequest>
        where TRequest : IRequest
    {
        /// <summary>
        /// Handles the request sent by the mediator.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}
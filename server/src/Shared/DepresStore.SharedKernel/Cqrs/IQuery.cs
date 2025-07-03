using DepresStore.SharedKernel.Mediator;

namespace DepresStore.SharedKernel.Cqrs
{
    /// <summary>
    /// Defines a query request.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}
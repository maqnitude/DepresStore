using DepresStore.Shared.Kernel.Mediator;

namespace DepresStore.Shared.Kernel.Cqrs
{
    /// <summary>
    /// Defines a query request.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}
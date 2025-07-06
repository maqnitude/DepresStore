using DepresStore.Shared.Kernel.Mediator;

namespace DepresStore.Shared.Kernel.Cqrs
{
    /// <summary>
    /// Defines a query request.
    /// </summary>
    /// <typeparam name="TResult">Query result type.</typeparam>
    public interface IQuery<out TResult> : IRequest<TResult> { }
}
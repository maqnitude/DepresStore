using DepresStore.Shared.Kernel.Common;

namespace DepresStore.Shared.Kernel.Application
{
    /// <summary>
    /// Defines a query request.
    /// </summary>
    /// <typeparam name="TResult">Query result type.</typeparam>
    public interface IQuery<out TResult> : IRequest<TResult> { }
}
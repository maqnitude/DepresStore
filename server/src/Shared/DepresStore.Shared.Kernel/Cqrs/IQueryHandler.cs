using DepresStore.Shared.Kernel.Mediator;

namespace DepresStore.Shared.Kernel.Cqrs
{
    /// <summary>
    /// Defines a handler for a query with a result.
    /// </summary>
    /// <typeparam name="TQuery">Query type.</typeparam>
    /// <typeparam name="TResult">Query result type.</typeparam>
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
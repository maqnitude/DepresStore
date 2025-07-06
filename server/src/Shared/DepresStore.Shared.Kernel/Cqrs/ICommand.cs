using DepresStore.Shared.Kernel.Mediator;

namespace DepresStore.Shared.Kernel.Cqrs
{
    /// <summary>
    /// Defines a command request with a result.
    /// </summary>
    /// <typeparam name="TResult">Command result type.</typeparam>
    public interface ICommand<out TResult> : IRequest<TResult> { }

    /// <summary>
    /// Defines a command request with no result.
    /// </summary>
    public interface ICommand : IRequest { }
}
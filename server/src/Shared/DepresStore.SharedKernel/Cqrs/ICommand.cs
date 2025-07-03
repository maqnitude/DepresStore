using DepresStore.SharedKernel.Mediator;

namespace DepresStore.SharedKernel.Cqrs
{
    /// <summary>
    /// Defines a command request with a response.
    /// </summary>
    /// <typeparam name="TResponse">Response type.</typeparam>
    public interface ICommand<out TResponse> : IRequest<TResponse> { }

    /// <summary>
    /// Defines a command request with no response.
    /// </summary>
    public interface ICommand : IRequest { }
}
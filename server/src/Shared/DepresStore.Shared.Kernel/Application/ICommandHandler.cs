using DepresStore.Shared.Kernel.Common;

namespace DepresStore.Shared.Kernel.Application
{
    /// <summary>
    /// Defines a handler for a command with a result.
    /// </summary>
    /// <typeparam name="TCommand">Command type.</typeparam>
    /// <typeparam name="TResult">Command result type.</typeparam>
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }

    /// <summary>
    /// Defines a handler for a command.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }
}
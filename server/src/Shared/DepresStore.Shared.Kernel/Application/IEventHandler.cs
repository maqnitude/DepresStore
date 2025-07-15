namespace DepresStore.Shared.Kernel.Application
{
    /// <summary>
    /// Defines an event handler.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<in TEvent>
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
    }
}
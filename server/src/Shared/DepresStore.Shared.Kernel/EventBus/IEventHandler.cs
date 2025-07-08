namespace DepresStore.Shared.Kernel.EventBus
{
    public interface IEventHandler<in TEvent>
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
    }
}
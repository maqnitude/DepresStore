namespace DepresStore.Shared.Kernel.Application
{
    public interface IEventHandler<in TEvent>
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
    }
}
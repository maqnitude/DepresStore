using DepresStore.Shared.Kernel.Common;

namespace DepresStore.Shared.Kernel.Infrastructure
{
    public interface IEventBus
    {
        /// <summary>
        /// Publishes event and invoke the handlers subscribed to the event.
        /// </summary>
        /// <typeparam name="TEvent">Event type.</typeparam>
        /// <param name="event">The event to publish.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : IEvent;
    }
}
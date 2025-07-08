namespace DepresStore.Shared.Kernel.EventBus
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

        /// <summary>
        /// Subscribes a handler to an event.
        /// </summary>
        /// <typeparam name="TEvent">Event type.</typeparam>
        /// <typeparam name="THandler">Handler type.</typeparam>
        void Subscribe<TEvent, THandler>()
            where TEvent : IEvent
            where THandler : IEventHandler<TEvent>;
    }
}
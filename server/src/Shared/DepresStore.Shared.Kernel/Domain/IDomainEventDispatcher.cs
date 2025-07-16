namespace DepresStore.Shared.Kernel.Domain
{
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatches one or more domain events.
        /// </summary>
        /// <typeparam name="TEvent">Event type.</typeparam>
        /// <param name="domainEvents">List of domain events.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task DispatchAsync<TEvent>(
            IEnumerable<TEvent> domainEvents,
            CancellationToken cancellationToken)
            where TEvent : IDomainEvent;

        /// <summary>
        /// Dispatches and clears domain events associated with an aggregate root.
        /// </summary>
        /// <typeparam name="TAggregateRoot">Aggregate root type.</typeparam>
        /// <param name="aggregateRoot">Aggregate root.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task DispatchAndClearAsync<TAggregateRoot>(
            TAggregateRoot aggregateRoot,
            CancellationToken cancellationToken = default)
            where TAggregateRoot : IAggregateRoot;
    }
}
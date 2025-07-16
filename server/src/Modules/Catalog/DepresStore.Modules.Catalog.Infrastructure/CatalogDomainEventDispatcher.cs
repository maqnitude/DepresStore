using DepresStore.Shared.Kernel.Domain;
using DepresStore.Shared.Kernel.Infrastructure;
using Microsoft.Extensions.Logging;

namespace DepresStore.Modules.Catalog.Infrastructure
{
    public class CatalogDomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<CatalogDomainEventDispatcher> _logger;

        public CatalogDomainEventDispatcher(
            IEventBus eventBus,
            ILogger<CatalogDomainEventDispatcher> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task DispatchAsync<TEvent>(
            IEnumerable<TEvent> domainEvents,
            CancellationToken cancellationToken)
            where TEvent : IDomainEvent
        {
            foreach (var domainEvent in domainEvents)
            {
                await _eventBus.PublishAsync(domainEvent, cancellationToken);
            }
        }

        public async Task DispatchAndClearAsync<TAggregateRoot>(
            TAggregateRoot aggregateRoot,
            CancellationToken cancellationToken = default)
            where TAggregateRoot : IAggregateRoot
        {
            _logger.LogInformation("Dispatching domain events from {AggregateRoot}...", aggregateRoot.GetType());
            await DispatchAsync(aggregateRoot.DomainEvents, cancellationToken);
            aggregateRoot.ClearDomainEvents();
        }
    }
}
using DepresStore.Shared.Kernel.EventBus;

namespace DepresStore.Shared.Kernel
{
    public abstract class AggregateRoot<TId> : Entity<TId> where TId : ValueObject
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        // For EF Core
        protected AggregateRoot() : base() { }

        protected AggregateRoot(TId id) : base(id) { }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
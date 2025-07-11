using DepresStore.Shared.Kernel.EventBus;

namespace DepresStore.Shared.Kernel
{
    public abstract class AggregateRoot<TId> : Entity<TId>
        where TId : ValueObject
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected AggregateRoot() : base() { }

        protected AggregateRoot(TId id) : base(id) { }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
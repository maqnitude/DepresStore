namespace DepresStore.Shared.Kernel.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected AggregateRoot() : base()
        {
        }

        protected AggregateRoot(TId id) : base(id)
        {
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        public void ClearDomainEvents() => _domainEvents.Clear();

        /// <summary>
        /// Adds a domain event to an internal list. Can only be used within the aggregate root.
        /// </summary>
        /// <param name="domainEvent"></param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
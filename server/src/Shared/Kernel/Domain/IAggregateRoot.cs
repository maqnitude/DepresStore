namespace DepresStore.Shared.Kernel.Domain
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
    }
}
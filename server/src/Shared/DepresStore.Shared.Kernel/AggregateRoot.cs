namespace DepresStore.Shared.Kernel
{
    public abstract class AggregateRoot<TId> : Entity<TId> where TId : ValueObject
    {
        // For EF Core
        protected AggregateRoot() : base() { }

        protected AggregateRoot(TId id) : base(id) { }
    }
}
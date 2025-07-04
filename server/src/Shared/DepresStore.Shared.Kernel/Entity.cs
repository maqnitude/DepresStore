namespace DepresStore.Shared.Kernel
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : ValueObject
    {
        public TId Id { get; protected set; } = null!;

        // For EF Core
        protected Entity() { }

        protected Entity(TId id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public bool Equals(Entity<TId>? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id.Equals(other.Id);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Entity<TId>);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
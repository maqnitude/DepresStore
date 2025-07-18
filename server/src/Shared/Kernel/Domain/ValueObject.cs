namespace DepresStore.Shared.Kernel.Domain
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public bool Equals(ValueObject? other)
        {
            if (other is null || GetType() != other.GetType())
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ValueObject);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();

            foreach (var component in GetEqualityComponents())
            {
                hashCode.Add(component);
            }

            return hashCode.ToHashCode();
        }

        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return !Equals(left, right);
        }
    }
}
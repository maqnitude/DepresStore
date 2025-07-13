using DepresStore.Shared.Kernel;

namespace DepresStore.Modules.Catalog.Domain.ValueObjects
{
    public class CategoryId : ValueObject
    {
        public Guid Value { get; init; }

        public CategoryId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("CategoryId value cannot be empty", nameof(value));
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
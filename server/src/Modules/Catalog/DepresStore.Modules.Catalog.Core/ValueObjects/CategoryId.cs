using DepresStore.Shared.Kernel;

namespace DepresStore.Modules.Catalog.Core.ValueObjects
{
    public class CategoryId : ValueObject
    {
        public Guid Value { get; private set; }

        // For EF Core
        private CategoryId() { }

        public CategoryId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("CategoryId value cannot be empty", nameof(value));
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
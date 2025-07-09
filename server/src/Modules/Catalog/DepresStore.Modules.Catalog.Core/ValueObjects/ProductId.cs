using DepresStore.Shared.Kernel;

namespace DepresStore.Modules.Catalog.Core.ValueObjects
{
    public class ProductId : ValueObject
    {
        public Guid Value { get; private set; }

        // For EF Core
        private ProductId() { }

        public ProductId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("ProductId value cannot be empty", nameof(value));
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
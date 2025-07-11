using DepresStore.Shared.Kernel;

namespace DepresStore.Modules.Inventory.Core.ValueObjects
{
    public class ProductVariantId : ValueObject
    {
        public Guid Value { get; private set; }

        private ProductVariantId() : base() { }

        public ProductVariantId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("ProductVariantId value cannot be empty", nameof(value));
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
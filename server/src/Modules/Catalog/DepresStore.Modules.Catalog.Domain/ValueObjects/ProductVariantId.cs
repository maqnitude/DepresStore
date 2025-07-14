using DepresStore.Shared.Kernel.Domain;

namespace DepresStore.Modules.Catalog.Domain.ValueObjects
{
    public class ProductVariantId : ValueObject
    {
        public Guid Value { get; init; }

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
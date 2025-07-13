using DepresStore.Modules.Catalog.Domain.Enums;
using DepresStore.Shared.Kernel;

namespace DepresStore.Modules.Catalog.Domain.ValueObjects
{
    public class ProductVariantAttribute : ValueObject
    {
        public required string Key { get; init; }

        public required string Value { get; init; }

        public required ProductVariantAttributeType Type { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Key;
            yield return Value;
            yield return Type;
        }
    }
}
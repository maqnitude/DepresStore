using DepresStore.Shared.Kernel;

namespace DepresStore.Modules.Inventory.Domain.ValueObjects
{
    public class StockId : ValueObject
    {
        public Guid Value { get; init; }

        public StockId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("StockId value cannot be empty", nameof(value));
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
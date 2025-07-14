using DepresStore.Shared.Kernel.Domain;

namespace DepresStore.Modules.Inventory.Domain.ValueObjects
{
    public class WarehouseId : ValueObject
    {
        public Guid Value { get; init; }

        public WarehouseId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("WarehouseId cannot be empty");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
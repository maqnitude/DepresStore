namespace DepresStore.Shared.Kernel.ValueObjects
{
    public class Money : ValueObject
    {
        public required decimal Amount { get; set; }

        public required string Currency { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
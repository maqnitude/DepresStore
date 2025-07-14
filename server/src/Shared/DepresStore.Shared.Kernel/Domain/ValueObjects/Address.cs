namespace DepresStore.Shared.Kernel.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public required string AddressLine1 { get; init; }

        public string? AddressLine2 { get; init; }

        public required string City { get; init; }

        public required string StateOrProvince { get; init; }

        public required string Country { get; init; }

        public required string PostalCode { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AddressLine1;

            if (AddressLine2 != null)
            {
                yield return AddressLine2;
            }

            yield return City;
            yield return StateOrProvince;
            yield return Country;
            yield return PostalCode;
        }
    }
}
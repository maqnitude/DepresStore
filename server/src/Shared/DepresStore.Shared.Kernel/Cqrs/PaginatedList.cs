namespace DepresStore.Shared.Kernel.Cqrs
{
    public class PaginatedList<TItem>
    {
        public List<TItem> Items { get; set; } = [];

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
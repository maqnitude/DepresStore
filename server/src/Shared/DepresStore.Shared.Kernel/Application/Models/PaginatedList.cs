namespace DepresStore.Shared.Kernel.Application.Models
{
    public class PaginatedList<TItem>
    {
        public List<TItem> Items { get; set; } = [];

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
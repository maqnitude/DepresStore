using DepresStore.Modules.Inventory.Domain.ValueObjects;
using DepresStore.Shared.Kernel;
using DepresStore.Shared.Kernel.ValueObjects;

namespace DepresStore.Modules.Inventory.Domain.Entities
{
    public class Warehouse : AggregateRoot<WarehouseId>
    {
        public required Address Location { get; set; }

        // Navigations
        public List<Stock> Stocks { get; set; } = [];
    }
}
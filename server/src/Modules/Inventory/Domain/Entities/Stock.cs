using DepresStore.Modules.Inventory.Domain.ValueObjects;
using DepresStore.Shared.Kernel.Domain;

namespace DepresStore.Modules.Inventory.Domain.Entities
{
    public class Stock : Entity<StockId>
    {
        public required WarehouseId WarehouseId { get; set; }

        public required string Sku { get; set; }

        public required int Quantity { get; set; }

        // Navigations
        public Warehouse Warehouse { get; set; } = null!;
    }
}
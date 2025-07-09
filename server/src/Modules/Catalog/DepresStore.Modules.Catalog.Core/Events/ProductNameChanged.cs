using DepresStore.Modules.Catalog.Core.ValueObjects;
using DepresStore.Shared.Kernel.EventBus;

namespace DepresStore.Modules.Catalog.Core.Events
{
    public record ProductNameChanged(ProductId ProductId, string OldName, string NewName) : IDomainEvent { }
}
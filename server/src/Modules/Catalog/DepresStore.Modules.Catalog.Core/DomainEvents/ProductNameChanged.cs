using DepresStore.Shared.Kernel.EventBus;
using DepresStore.Shared.Kernel.ValueObjects;

namespace DepresStore.Modules.Catalog.Core.DomainEvents
{
    public record ProductNameChanged(ProductId ProductId, string OldName, string NewName) : IDomainEvent { }
}
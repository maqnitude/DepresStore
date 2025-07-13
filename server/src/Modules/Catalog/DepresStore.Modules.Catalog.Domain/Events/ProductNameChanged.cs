using DepresStore.Modules.Catalog.Domain.ValueObjects;
using DepresStore.Shared.Kernel.EventBus;

namespace DepresStore.Modules.Catalog.Domain.Events
{
    public record ProductNameChanged(ProductId ProductId, string OldName, string NewName) : IDomainEvent { }
}
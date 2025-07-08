using DepresStore.Shared.Kernel.EventBus;

namespace DepresStore.Modules.Catalog.Core.ProductAggregate.Events
{
    public record ProductNameChangedEvent(ProductId ProductId, string OldName, string NewName) : IDomainEvent { }
}
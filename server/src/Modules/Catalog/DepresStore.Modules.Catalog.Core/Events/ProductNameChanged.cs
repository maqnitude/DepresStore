using DepresStore.Modules.Catalog.Core.ProductAggregate;
using DepresStore.Shared.Kernel.EventBus;

namespace DepresStore.Modules.Catalog.Core.Events
{
    public record ProductNameChanged(ProductId ProductId, string OldName, string NewName) : IDomainEvent { }
}
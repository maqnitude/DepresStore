namespace DepresStore.Shared.Kernel.EventBus
{
    /// <summary>
    /// Represents the base event type.
    /// </summary>
    public interface IEvent { }

    /// <summary>
    /// Represents a domain event.
    /// </summary>
    public interface IDomainEvent : IEvent { }

    /// <summary>
    /// Represents an integration event.
    /// </summary>
    public interface IIntegrationEvent : IEvent { }
}
using System.Collections.Concurrent;
using DepresStore.Shared.Kernel.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DepresStore.Shared.Infrastructure
{
    public class InProcessEventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<InProcessEventBus> _logger;

        private readonly ConcurrentDictionary<Type, List<Type>> _eventHandlers = new();

        public InProcessEventBus(
            IServiceProvider serviceProvider,
            ILogger<InProcessEventBus> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task PublishAsync<TEvent>(
            TEvent @event,
            CancellationToken cancellationToken = default)
            where TEvent : IEvent
        {
            var eventType = @event.GetType();

            _logger.LogInformation("Publishing event {EventType}...", eventType);

            if (!_eventHandlers.TryGetValue(eventType, out var handlerInterfaceTypes))
            {
                _logger.LogWarning("No handler for event {EventType} found", eventType);
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            foreach (var handlerInterfaceType in handlerInterfaceTypes)
            {
                try
                {
                    _logger.LogInformation("Getting concrete handler of {HandlerInterfaceType}...", handlerInterfaceType);

                    var handler = scope.ServiceProvider.GetRequiredService(handlerInterfaceType);

                    _logger.LogInformation("Getting HandleAsync method from {HandlerType}...", handler.GetType());

                    var handleMethod = handlerInterfaceType.GetMethod("HandleAsync");
                    if (handleMethod != null)
                    {
                        _logger.LogInformation("Invoking HandleAsync method from {HandlerType}...", handler.GetType());

                        var task = (Task)handleMethod.Invoke(handler, [@event, cancellationToken])!;
                        await task;
                    }
                    else
                    {
                        _logger.LogError("Could not find HandleAsync method from {HandlerType}", handler.GetType());
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Could not get or invoke concrete handler of {HandlerInterfaceType}", handlerInterfaceType);
                }
            }
        }

        public void Subscribe<TEvent, THandler>()
            where TEvent : IEvent
            where THandler : IEventHandler<TEvent>
        {
            var eventType = typeof(TEvent);
            // Event handlers are registered via the IEventHandler interface
            // not the concrete implementation directly
            var handlerInterfaceType = typeof(IEventHandler<TEvent>);

            _logger.LogInformation("Subscribing event handler {HandlerInterfaceType} to event {EventType}...", handlerInterfaceType, eventType);

            _eventHandlers.AddOrUpdate(
                eventType,
                _ => [handlerInterfaceType],
                (_, handlerInterfaceTypes) =>
                {
                    // List<Type> is not thread-safe
                    lock (handlerInterfaceTypes)
                    {
                        if (!handlerInterfaceTypes.Contains(handlerInterfaceType))
                        {
                            handlerInterfaceTypes.Add(handlerInterfaceType);
                        }
                    }

                    return handlerInterfaceTypes;
                });
        }
    }
}
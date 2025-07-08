using DepresStore.Shared.Kernel.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DepresStore.Shared.Infrastructure
{
    public class InProcessEventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<InProcessEventBus> _logger;

        public InProcessEventBus(
            IServiceProvider serviceProvider,
            ILogger<InProcessEventBus> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        /// <summary>
        /// Publish event and invoked handlers registered in DI container.
        /// </summary>
        /// <typeparam name="TEvent">Event type.</typeparam>
        /// <param name="event">The event to publish.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        public async Task PublishAsync<TEvent>(
            TEvent @event,
            CancellationToken cancellationToken = default)
            where TEvent : IEvent
        {
            var eventType = @event.GetType();

            _logger.LogInformation("Publishing event {EventType}...", eventType);

            using var scope = _serviceProvider.CreateScope();

            _logger.LogInformation("Getting handlers for event {Event}...", eventType);

            var handlerGenericType = typeof(IEventHandler<>).MakeGenericType(eventType);
            var handlers = scope.ServiceProvider.GetServices(handlerGenericType);

            if (!handlers.Any())
            {
                _logger.LogWarning("No handlers for event {Event} found", eventType);
                return;
            }

            _logger.LogInformation("Getting HandleAsync method from {HandlerType}...", handlerGenericType);

            var handleMethod = handlerGenericType.GetMethod("HandleAsync");
            if (handleMethod == null)
            {
                _logger.LogWarning("Could not find HandlerAsync method from {HandlerType}", handlerGenericType);
                return;
            }

            foreach (var handler in handlers)
            {
                try
                {
                    _logger.LogInformation("Invoking HandleAsync method from handler {HandlerType} for event {Event}...",
                        handlerGenericType, eventType);

                    await (Task)handleMethod.Invoke(handler, [@event, cancellationToken])!;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Could not invoke HandleAsync method from handler {HandlerType} for event {Event}",
                        handlerGenericType, eventType);
                }
            }
        }
    }
}
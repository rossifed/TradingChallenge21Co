using Microsoft.Extensions.DependencyInjection;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Shared.Infrastructure.Events
{
    internal sealed class EventDispatcher : IEventDispatcher
    {

        private readonly IServiceProvider ServiceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class, IEvent
        {
            using var scope = ServiceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
            var tasks = handlers.Select(handler => handler.HandleAsync(@event, cancellationToken));
            await Task.WhenAll(tasks);
        }


    }
}

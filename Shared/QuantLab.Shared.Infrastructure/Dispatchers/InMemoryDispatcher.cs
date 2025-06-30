using QuantLab.Shared.Abstractions.Commands;
using QuantLab.Shared.Abstractions.Dispatchers;
using QuantLab.Shared.Abstractions.Events;
using QuantLab.Shared.Abstractions.Queries;
namespace QuantLab.Shared.Infrastructure.Dispatchers
{
    public class InMemoryDispatcher : IDispatcher
    {
        private IEventDispatcher EventDispatcher { get; }
        private IQueryDispatcher QueryDispatcher { get; }
        private ICommandDispatcher CommandDispatcher { get; }
        public InMemoryDispatcher(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IEventDispatcher eventDispatcher)
        {
            this.QueryDispatcher = queryDispatcher;
            this.CommandDispatcher = commandDispatcher;
            this.EventDispatcher = eventDispatcher;
        }
        public Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand
            => CommandDispatcher.SendAsync(command, cancellationToken);

        public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
            => EventDispatcher.PublishAsync(@event, cancellationToken);

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
            => QueryDispatcher.QueryAsync(query, cancellationToken);
    }
}

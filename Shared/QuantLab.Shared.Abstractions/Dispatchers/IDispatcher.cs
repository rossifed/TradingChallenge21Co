using QuantLab.Shared.Abstractions.Commands;
using QuantLab.Shared.Abstractions.Events;
using QuantLab.Shared.Abstractions.Queries;

namespace QuantLab.Shared.Abstractions.Dispatchers
{
    public interface IDispatcher
    {

        Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
        Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
    }
}

using QuantLab.Shared.Abstractions.Messaging;
namespace QuantLab.Shared.Infrastructure.Messaging
{
    internal interface IAsyncMessageDispatcher
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class, IMessage;
    }

    internal class AsyncMessageDispatcher : IAsyncMessageDispatcher
    {
        private IMessageChannel MessageChannel { get; }

        public AsyncMessageDispatcher(IMessageChannel messageChannel)
        {
            this.MessageChannel = messageChannel;
        }
        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class, IMessage
        => await MessageChannel.Writer.WriteAsync(new MessageEnvelope(message), cancellationToken);

    }
}

using Microsoft.Extensions.Logging;
using QuantLab.Shared.Abstractions.Messaging;
namespace QuantLab.Shared.Infrastructure.Messaging
{
    internal class InMemoryMessageBroker : IMessageBroker
    {

        private ILogger<InMemoryMessageBroker> Logger { get; }

        private IAsyncMessageDispatcher AsyncMessageDispatcher { get; }
        private MessagingOptions MessagingOptions { get; }
        public InMemoryMessageBroker(IAsyncMessageDispatcher asyncMessageDispatcher, MessagingOptions messagingOptions, ILogger<InMemoryMessageBroker> logger)
        {

            this.MessagingOptions = messagingOptions;
            this.Logger = logger;
            this.AsyncMessageDispatcher = asyncMessageDispatcher;

        }
        public async Task PublishAsync(IMessage message, CancellationToken cancellationToken)
        => await PublishAsync(cancellationToken, message);


        public async Task PublishAsync(IMessage[] messages, CancellationToken cancellationToken)
         => await PublishAsync(cancellationToken, messages);


        private async Task PublishAsync(CancellationToken cancellationToken, params IMessage[] messages)
        {
            if (messages == null)
            {
                return;
            }
            messages = messages.Where(x => x is not null).ToArray();

            var tasks = messages.Select(message => AsyncMessageDispatcher.PublishAsync(message, cancellationToken));


            await Task.WhenAll(tasks);
        }
    }
}

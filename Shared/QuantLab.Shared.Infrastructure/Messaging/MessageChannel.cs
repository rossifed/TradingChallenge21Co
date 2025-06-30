using System.Threading.Channels;
namespace QuantLab.Shared.Infrastructure.Messaging
{
    internal class MessageChannel : IMessageChannel
    {
        private Channel<MessageEnvelope> Messages = Channel.CreateUnbounded<MessageEnvelope>();
        public ChannelReader<MessageEnvelope> Reader => Messages.Reader;

        public ChannelWriter<MessageEnvelope> Writer => Messages.Writer;
    }
}

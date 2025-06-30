using System.Threading.Channels;
namespace QuantLab.Shared.Infrastructure.Messaging
{
    internal interface IMessageChannel
    {

        ChannelReader<MessageEnvelope> Reader { get; }
        ChannelWriter<MessageEnvelope> Writer { get; }
    }


}

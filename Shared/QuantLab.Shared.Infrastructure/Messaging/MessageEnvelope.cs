using QuantLab.Shared.Abstractions.Messaging;
namespace QuantLab.Shared.Infrastructure.Messaging
{
    internal record MessageEnvelope(IMessage message);
}

using QuantLab.Shared.Abstractions.Messaging;

namespace QuantLab.Shared.Infrastructure.Messaging.Contexts;

public interface IMessageContextRegistry
{
    void Set(IMessage message, IMessageContext context);
}
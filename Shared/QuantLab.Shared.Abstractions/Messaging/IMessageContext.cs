using QuantLab.Shared.Abstractions.Contexts;

namespace QuantLab.Shared.Abstractions.Messaging
{

    public interface IMessageContext
    {
        public Guid MessageId { get; }
        public IContext Context { get; }
    }
}
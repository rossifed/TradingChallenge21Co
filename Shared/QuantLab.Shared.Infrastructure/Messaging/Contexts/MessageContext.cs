﻿using QuantLab.Shared.Abstractions.Contexts;
using QuantLab.Shared.Abstractions.Messaging;

namespace QuantLab.Shared.Infrastructure.Messaging.Contexts;

public class MessageContext : IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }

    public MessageContext(Guid messageId, IContext context)
    {
        MessageId = messageId;
        Context = context;
    }
}
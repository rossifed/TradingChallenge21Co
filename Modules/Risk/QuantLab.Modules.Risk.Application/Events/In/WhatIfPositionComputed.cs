using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.Risk.Application.Dtos
{
    internal record WhatIfPositionComputed(OrderDto Order, PositionDto Position, PositionPnLDto Pnl) : IEvent;

}

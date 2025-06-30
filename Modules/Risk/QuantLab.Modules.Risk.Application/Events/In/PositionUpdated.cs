using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.Risk.Application.Dtos
{
    internal record PositionUpdated(PositionDto Position, PositionPnLDto PositionPnL) : IEvent;

}

using QuantLab.Modules.StateTracking.Application.Dtos;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.StateTracking.Application.Events.Out
{
    internal record TradeExecuted(TradeDto Trade) : IEvent;
}

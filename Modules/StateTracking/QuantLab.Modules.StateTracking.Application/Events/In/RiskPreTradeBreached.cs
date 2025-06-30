using QuantLab.Modules.StateTracking.Application.Dtos;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.StateTracking.Application.Events.Out
{
    record class RiskPreTradeBreached(OrderDto Order, IEnumerable<ConstraintBreachDto> ConstraintBreaches) : IEvent;

}

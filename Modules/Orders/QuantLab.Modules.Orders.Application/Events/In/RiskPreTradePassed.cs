using QuantLab.Modules.Orders.Application.Dtos;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.Orders.Application.Events.Out
{
    record class RiskPreTradePassed(OrderDto order) : IEvent;

}

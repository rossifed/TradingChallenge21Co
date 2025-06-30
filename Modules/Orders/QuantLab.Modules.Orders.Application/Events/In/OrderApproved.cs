using QuantLab.Modules.Orders.Application.Dtos;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.Orders.Application.Events.In
{
    record class OrderApproved(OrderDto order) : IEvent;

}

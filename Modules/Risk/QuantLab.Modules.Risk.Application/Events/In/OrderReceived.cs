using QuantLab.Modules.Risk.Application.Dtos;
using QuantLab.Shared.Abstractions.Events;
namespace QuantLab.Modules.Risk.Application.Events.In
{
    public record OrderReceived(OrderDto Order) : IEvent;
}

using QuantLab.Modules.Orders.Application.Dtos;

namespace QuantLab.Modules.Orders.Application.Services
{
    internal interface IOrderUpdateHandler
    {

        Task HandleAsync(OrderUpdateDto orderUpdateDto);
    }
}

using QuantLab.Modules.Orders.Application.Dtos;

namespace QuantLab.Modules.Orders.Application.Services
{
    internal interface IOrderPlacementService
    {


        Task<string> PlaceOrderAsync(OrderDto order);
    }
}

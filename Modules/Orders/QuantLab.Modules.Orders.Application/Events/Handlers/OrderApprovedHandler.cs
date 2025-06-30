using QuantLab.Modules.Orders.Application.Events.In;
using QuantLab.Modules.Orders.Application.Services;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.Orders.Application.Events.Handlers
{
    internal class OrderApprovedHandler : IEventHandler<OrderApproved>
    {
        private readonly IOrderManagementService _orderManagementService;

        public OrderApprovedHandler(IOrderManagementService orderManagementService)
        {
            _orderManagementService = orderManagementService;
        }

        public async Task HandleAsync(OrderApproved @event, CancellationToken cancellationToken = default)
        {
            await _orderManagementService.PlaceOrderAsync(@event.order);
        }
    }
}

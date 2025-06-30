using QuantLab.Modules.Orders.Application.Events.Out;
using QuantLab.Modules.Orders.Application.Services;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.Orders.Application.Events.Handlers
{
    internal class RiskPreTradePassedHandler : IEventHandler<RiskPreTradePassed>
    {
        private readonly IOrderManagementService _orderManagementService;

        public RiskPreTradePassedHandler(IOrderManagementService orderManagementService)
        {
            _orderManagementService = orderManagementService;
        }

        public async Task HandleAsync(RiskPreTradePassed @event, CancellationToken cancellationToken = default)
        {
            await _orderManagementService.PlaceOrderAsync(@event.order);
        }
    }
}

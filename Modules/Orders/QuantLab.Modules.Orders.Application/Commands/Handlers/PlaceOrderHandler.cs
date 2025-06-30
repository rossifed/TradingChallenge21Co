using QuantLab.Modules.Orders.Application.Commands.In;
using QuantLab.Modules.Orders.Application.Services;
using QuantLab.Shared.Abstractions.Commands;

namespace QuantLab.Modules.Orders.Application.Commands.Handlers
{
    internal class PlaceOrderHandler : ICommandHandler<PlaceOrder>
    {

        private readonly IOrderManagementService _orderManagementService;

        public PlaceOrderHandler(IOrderManagementService orderManagementService)
        {
            _orderManagementService = orderManagementService;
        }

        public async Task HandleAsync(PlaceOrder command, CancellationToken cancellationToken = default)
        {
            await _orderManagementService.CreateOrder(command.Symbol, command.Quantity);
        }
    }
}

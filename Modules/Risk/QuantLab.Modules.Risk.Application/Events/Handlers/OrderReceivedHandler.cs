using QuantLab.Modules.Risk.Application.Events.In;
using QuantLab.Modules.Risk.Application.Services;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.Risk.Application.Events.Handlers
{
    internal class OrderReceivedHandler : IEventHandler<OrderReceived>
    {
        private readonly IRiskManagementService _riskManagementService;

        public OrderReceivedHandler(IRiskManagementService riskManagementService)
        {
            _riskManagementService = riskManagementService;
        }

        public async Task HandleAsync(OrderReceived @event, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }
}

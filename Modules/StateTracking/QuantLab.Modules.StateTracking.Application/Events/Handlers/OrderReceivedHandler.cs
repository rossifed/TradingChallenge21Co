using QuantLab.Modules.StateTracking.Application.Events.Out;
using QuantLab.Modules.StateTracking.Application.Mappers;
using QuantLab.Modules.StateTracking.Application.Services;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.StateTracking.Application.Events.Handlers
{
    internal class OrderReceivedHandler : IEventHandler<OrderReceived>
    {
        private IPositionManagementService _positionManagementService;

        public OrderReceivedHandler(IPositionManagementService positionManagementService)
        {
            _positionManagementService = positionManagementService;
        }

        public async Task HandleAsync(OrderReceived @event, CancellationToken cancellationToken = default)
        {

            await _positionManagementService.ComputedWhatIfPositionAsync(@event.Order.Map());
        }
    }
}

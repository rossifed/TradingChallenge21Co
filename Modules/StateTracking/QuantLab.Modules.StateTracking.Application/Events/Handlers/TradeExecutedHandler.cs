using QuantLab.Modules.StateTracking.Application.Events.Out;
using QuantLab.Modules.StateTracking.Application.Mappers;
using QuantLab.Modules.StateTracking.Application.Services;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.StateTracking.Application.Events.Handlers
{
    internal class TradeExecutedHandler : IEventHandler<TradeExecuted>
    {

        private IPositionManagementService _positionManagementService;
        private ITradeCaptureService _tradeCaptureService;

        public TradeExecutedHandler(IPositionManagementService positionManagementService, ITradeCaptureService tradeCaptureService)
        {
            _positionManagementService = positionManagementService;
            _tradeCaptureService = tradeCaptureService;
        }

        public async Task HandleAsync(TradeExecuted @event, CancellationToken cancellationToken = default)
        {
            await _tradeCaptureService.OnNewTradeAsync(@event.Trade);//These should be on different bounded context but to follow the guideline i do it here
            await _positionManagementService.UpdatePositionAsync(@event.Trade.Map());
        }
    }
}

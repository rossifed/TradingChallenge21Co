using QuantLab.Modules.StateTracking.Application.Events.Out;
using QuantLab.Modules.StateTracking.Application.Mappers;
using QuantLab.Modules.StateTracking.Application.Services;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.StateTracking.Application.Events.Handlers
{
    internal class BestBidOfferHandler : IEventHandler<BestBidOfferEvent>
    {

        private readonly IPositionManagementService _positionManagementService;

        public BestBidOfferHandler(IPositionManagementService positionManagementService)
        {
            _positionManagementService = positionManagementService;
        }

        public async Task HandleAsync(BestBidOfferEvent @event, CancellationToken cancellationToken = default)
        {
            await _positionManagementService.UpdatePositionAsync(@event.BestBidOffer.Map());
        }
    }
}

using Microsoft.Extensions.Logging;
using QuantLab.Modules.Orders.Application.Events.In;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.Orders.Application.Events.Handlers
{
    internal class BestBidOfferEventHandler : IEventHandler<BestBidOfferEvent>
    {
        private readonly ILogger<BestBidOfferEventHandler> _logger;

        public BestBidOfferEventHandler(ILogger<BestBidOfferEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(BestBidOfferEvent @event, CancellationToken cancellationToken = default)
        {
            //  _logger.LogInformation($"{ @event.BestBidOffer}");
        }
    }
}

using Microsoft.Extensions.Logging;
using QuantLab.Modules.MarketData.Application.Services;
using QuantLab.Shared.Abstractions.Commands;

namespace QuantLab.Modules.MarketData.Application.Commands.Handlers
{
    internal class UnSubscribeToBestBidOfferHandler : ICommandHandler<UnsubscribeToBestBidOffer>
    {

        private readonly IBestBidOfferService _bestBidOfferService;
        private readonly ILogger<UnSubscribeToBestBidOfferHandler> _logger;

        public UnSubscribeToBestBidOfferHandler(IBestBidOfferService bestBidOfferService, ILogger<UnSubscribeToBestBidOfferHandler> logger)
        {
            _bestBidOfferService = bestBidOfferService;
            _logger = logger;
        }

        public async Task HandleAsync(UnsubscribeToBestBidOffer command, CancellationToken cancellationToken = default)
        {
            await _bestBidOfferService.UnsubscribeAllAsync(cancellationToken);
        }
    }
}

using Microsoft.Extensions.Logging;
using QuantLab.Modules.MarketData.Application.Commands.In;
using QuantLab.Modules.MarketData.Application.Services;
using QuantLab.Shared.Abstractions.Commands;

namespace QuantLab.Modules.MarketData.Application.Commands.Handlers
{
    internal class SubscribeToBestBidOfferHandler : ICommandHandler<SubscribeToBestBidOffer>
    {

        private readonly IBestBidOfferService _bestBidOfferService;
        private readonly ILogger<SubscribeToBestBidOfferHandler> _logger;

        public SubscribeToBestBidOfferHandler(IBestBidOfferService bestBidOfferService, ILogger<SubscribeToBestBidOfferHandler> logger)
        {
            _bestBidOfferService = bestBidOfferService;
            _logger = logger;
        }

        public async Task HandleAsync(SubscribeToBestBidOffer command, CancellationToken cancellationToken = default)
        {
            await _bestBidOfferService.SubscribeAsync(command.Symbol);
        }
    }
}

using Microsoft.Extensions.Logging;
using QuantLab.Modules.MarketData.Application.Dtos;
using QuantLab.Modules.MarketData.Application.Events.Out;
using QuantLab.Shared.Abstractions.Messaging;

namespace QuantLab.Modules.MarketData.Application.Services
{
    internal class BestBidOfferEventHandler : IMarketDataEventHandler<BestBidOfferDto>
    {
        private readonly ILogger<BestBidOfferEventHandler> _logger;
        private IMessageBroker MessageBroker { get; }
        private long _lastLogTicks;
        public BestBidOfferEventHandler(ILogger<BestBidOfferEventHandler> logger, IMessageBroker messageBroker)
        {
            _logger = logger;
            MessageBroker = messageBroker;
            _lastLogTicks = DateTimeOffset.MinValue.Ticks;
        }

        public async Task HandleAsync(BestBidOfferDto dto)
        {
            BestBidOfferEvent bestBidOfferEvent = new BestBidOfferEvent(dto);
            await MessageBroker.PublishAsync(bestBidOfferEvent);


            // 2) Throttle non-bloquant sur le logging
            var nowTicks = DateTimeOffset.UtcNow.Ticks;
            var lastTicks = Interlocked.Read(ref _lastLogTicks);
            if (nowTicks - lastTicks >= TimeSpan.TicksPerSecond)
            {
                // on essaie atomiquement de mettre à jour _lastLogTicks
                // uniquement si sa valeur n'a pas bougé depuis qu'on l'a lue
                if (Interlocked.CompareExchange(ref _lastLogTicks, nowTicks, lastTicks) == lastTicks)
                {
                    // si on est arrivé ici, on est le premier thread à passer après 1 sec
                    _logger.LogInformation("{@BestBidOffer}", dto);
                }
            }

        }
    }
}

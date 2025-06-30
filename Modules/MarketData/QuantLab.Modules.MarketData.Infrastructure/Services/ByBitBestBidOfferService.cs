using Bybit.Net.Clients;
using Bybit.Net.Objects.Models.V5;
using CryptoExchange.Net.Objects.Sockets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QuantLab.Modules.MarketData.Application.Dtos;
using QuantLab.Modules.MarketData.Application.Services;

namespace QuantLab.Modules.MarketData.Infrastructure.Services
{
    internal class ByBitBestBidOfferService : BackgroundService, IBestBidOfferService
    {
        private readonly IMarketDataEventHandler<BestBidOfferDto> _marketDataEventHandler;
        private readonly BybitSocketClient _socketClient;
        private readonly ILogger<ByBitBestBidOfferService> _logger;

        public ByBitBestBidOfferService(BybitSocketClient socketClient,
            IMarketDataEventHandler<BestBidOfferDto> marketDataEventHandler,
            ILogger<ByBitBestBidOfferService> logger)
        {
            _marketDataEventHandler = marketDataEventHandler;
            _socketClient = socketClient;
            _logger = logger;


        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //  await SubscribeAsync("BTCUSDT", stoppingToken);
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping ByBitBestBidOfferService...");
            // Se désabonner proprement et libérer le client
            await UnsubscribeAllAsync(cancellationToken);
            _socketClient.Dispose();
            await base.StopAsync(cancellationToken);
        }
        public async Task UnsubscribeAllAsync(CancellationToken cancellationToken)
        {
            await _socketClient.UnsubscribeAllAsync();
        }

        private async Task HandleDataEvent(DataEvent<BybitOrderbook> update)
        {
            try
            {
                var bid = update.Data.Bids[0];
                var ask = update.Data.Asks[0];
                var symbol = update.Data.Symbol;
                var timestamp = DateTime.UtcNow;
                var provider = "ByBit";
                BestBidOfferDto dto = new BestBidOfferDto(
                    BidPrice: bid.Price,
                    BidSize: bid.Quantity,
                    AskPrice: ask.Price,
                    AskSize: ask.Quantity,
                    MidPrice: (ask.Price + bid.Price) / 2, //Domain model should do but overkill for such simple computaion
                    Spread: (ask.Price - +bid.Price),
                    TimeStampUtc: timestamp,
                    Symbol: symbol,
                    Provider: provider
                    );

                await _marketDataEventHandler.HandleAsync(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling update for {Symbol}", update.Data.Symbol);
            }
        }
        public async Task SubscribeAsync(string symbol, CancellationToken cancellationToken = default)
        {

            var result = await _socketClient.V5SpotApi
                .SubscribeToOrderbookUpdatesAsync(symbol, 1, async e => await HandleDataEvent(e));

            if (!result.Success)
            {
                _logger.LogError(result.Error?.Message, "Failed to subscribe to {Symbol}", symbol);
                return;
            }
            else
            {

                _logger.LogInformation("Subscribed to {Symbol}", symbol);
            }


        }
    }
}

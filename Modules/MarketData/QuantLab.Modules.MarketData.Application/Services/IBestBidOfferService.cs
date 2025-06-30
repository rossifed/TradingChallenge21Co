namespace QuantLab.Modules.MarketData.Application.Services
{
    internal interface IBestBidOfferService
    {
        Task SubscribeAsync(string symbol, CancellationToken cancellationToken = default);
        Task UnsubscribeAllAsync(CancellationToken cancellationToken = default);
    }
}

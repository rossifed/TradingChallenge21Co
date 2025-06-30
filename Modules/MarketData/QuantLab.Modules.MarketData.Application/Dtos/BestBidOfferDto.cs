namespace QuantLab.Modules.MarketData.Application.Dtos
{
    public sealed record BestBidOfferDto(
        string Provider,
        string Symbol,
        decimal BidPrice,
        decimal BidSize,
        decimal AskPrice,
        decimal AskSize,
        decimal MidPrice,
        decimal Spread,
        DateTime TimeStampUtc);
}

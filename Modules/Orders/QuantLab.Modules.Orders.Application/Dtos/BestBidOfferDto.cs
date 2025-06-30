namespace QuantLab.Modules.Orders.Application.Dtos
{
    public sealed record BestBidOfferDto(
        string Provider,
        string Symbol,
        decimal BidPrice,
        decimal BidSize,
        decimal AskPrice,
        decimal AskSize,
        DateTime TimeStampUtc);
}

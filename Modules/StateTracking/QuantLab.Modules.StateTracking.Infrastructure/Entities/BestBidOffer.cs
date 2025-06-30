namespace QuantLab.Modules.StateTracking.Infrastructure.Entities
{
    public record BestBidOffer(
         string Symbol,
         decimal BidPrice,
         decimal AskPrice,
         decimal BidSize,
         decimal AskSize,
         decimal MidPrice,
         decimal Spread,
         DateTime TimeStampUtc);
}

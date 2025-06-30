namespace QuantLab.Modules.StateTracking.Domain.Model
{

    internal record BestBidOffer(
   string Symbol,
   decimal BidPrice,
   decimal AskPrice,
   decimal BidSize,
   decimal AskSize,
   decimal MidPrice,
   decimal Spread,
   DateTime TimeStampUtc
);



}

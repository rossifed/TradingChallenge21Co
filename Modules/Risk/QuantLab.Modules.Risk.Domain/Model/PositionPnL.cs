namespace QuantLab.Modules.Risk.Domain.Model
{
    internal record PositionPnL(
            Guid PositionId,
            decimal UnrealizedPnL,
            decimal RealizedPnL,
            decimal DailyUnrealizedPnL,
            decimal DailyRealizedPnL,
            decimal MarketPrice,
            decimal TotalPnL,
            decimal TotalDailyPnL,
            DateTime ComputedOnUtc);
}

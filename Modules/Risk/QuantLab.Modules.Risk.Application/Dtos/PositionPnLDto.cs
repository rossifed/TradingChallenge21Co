namespace QuantLab.Modules.Risk.Application.Dtos
{
    internal record PositionPnLDto(Guid PositionId,
     decimal UnrealizedPnL,
     decimal RealizedPnL,
     decimal DailyUnrealizedPnL,
     decimal DailyRealizedPnL,
     decimal TotalPnL,
     decimal TotalDailyPnL,
     decimal MarketPrice,
     DateTime ComputedOnUtc);
}

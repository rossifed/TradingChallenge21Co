using System.ComponentModel.DataAnnotations;

namespace QuantLab.Modules.StateTracking.Infrastructure.Entities
{
    public record PositionPnL([property: Key] Guid PositionId,
        decimal UnrealizedPnL,
        decimal RealizedPnL,
        decimal DailyUnrealizedPnL,
        decimal DailyRealizedPnL,
        decimal TotalPnL,
        decimal TotalDailyPnL,
        decimal MarketPrice,
        DateTime ComputedOn)
    {
        // navigation back to Position
        public Position Position { get; init; } = null!;
    }

}

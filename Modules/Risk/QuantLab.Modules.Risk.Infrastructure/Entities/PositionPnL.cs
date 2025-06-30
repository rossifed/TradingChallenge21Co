using System.ComponentModel.DataAnnotations;

namespace QuantLab.Modules.Risk.Infrastructure.Entities
{
    public record PositionPnL([property: Key] Guid PositionId, decimal UnrealizedPnL, decimal RealizedPnL, decimal MarketPrice, DateTime ComputedOn)
    {
        // navigation back to Position
        public Position Position { get; init; } = null!;
    }

}

using System.ComponentModel.DataAnnotations;

namespace QuantLab.Modules.StateTracking.Infrastructure.Entities
{
    public record Position([property: Key] Guid Id, string Symbol, decimal Quantity, decimal EntryPrice)
    {
        // navigation prop for EF
        public PositionPnL PositionPnL { get; init; } = null!;
    }
}

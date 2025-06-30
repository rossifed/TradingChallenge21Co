using System.ComponentModel.DataAnnotations;

namespace QuantLab.Modules.StateTracking.Infrastructure.Entities
{
    public record PositionAggregate([property: Key] Guid PositionId, Position Position, PositionPnL PositionPnL);
}

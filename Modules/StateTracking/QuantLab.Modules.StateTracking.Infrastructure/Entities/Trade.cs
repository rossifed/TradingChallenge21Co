using System.ComponentModel.DataAnnotations;

namespace QuantLab.Modules.StateTracking.Infrastructure.Entities
{
    public record Trade(
        [property: Key]
             Guid TradeId,
        Guid OrderId,
        string Symbol,
        decimal Quantity,
        decimal FilledQuantity,
        decimal ExecPrice,
        decimal PlacedPrice,
        DateTime PlacedOn,
        DateTime FilledOn);

}

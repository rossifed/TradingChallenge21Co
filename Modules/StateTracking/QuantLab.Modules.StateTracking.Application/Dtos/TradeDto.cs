namespace QuantLab.Modules.StateTracking.Application.Dtos
{
    public record TradeDto(
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

namespace QuantLab.Modules.Orders.Application.Dtos
{

    internal record OrderUpdateDto(
        Guid OrderId,
        string Symbol,
        string Status,
        string Side,
        decimal Quantity,
        decimal FilledQuantity,
        decimal FilledPrice,
        decimal PlacedPrice,
        DateTime PlacedOn,
        DateTime FilledOn);
}

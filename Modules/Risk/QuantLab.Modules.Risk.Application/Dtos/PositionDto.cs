namespace QuantLab.Modules.Risk.Application.Dtos
{
    internal record PositionDto(Guid Id, string Symbol, decimal Quantity, decimal EntryPrice);

}

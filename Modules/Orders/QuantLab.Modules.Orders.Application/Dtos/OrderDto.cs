namespace QuantLab.Modules.Orders.Application.Dtos
{
    public record OrderDto(Guid Id, string Symbol, decimal Quantity);
}

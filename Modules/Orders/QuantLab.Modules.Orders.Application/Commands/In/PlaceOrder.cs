using QuantLab.Shared.Abstractions.Commands;
using System.ComponentModel;
namespace QuantLab.Modules.Orders.Application.Commands.In
{
    public record PlaceOrder : ICommand
    {
        [DefaultValue("BTCUSDT")]
        public string Symbol { get; init; } = "BTCUSDT";

        [DefaultValue(1)]
        public decimal Quantity { get; init; } = 1;
    }
}

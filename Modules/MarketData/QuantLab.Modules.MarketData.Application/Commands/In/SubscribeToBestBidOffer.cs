using QuantLab.Shared.Abstractions.Commands;
namespace QuantLab.Modules.MarketData.Application.Commands.In
{
    public sealed record SubscribeToBestBidOffer(string Symbol = "BTCUSDT") : ICommand;
}

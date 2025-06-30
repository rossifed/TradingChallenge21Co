namespace QuantLab.Modules.MarketData.Application.Services
{
    internal interface IMarketDataEventHandler<TEvent>
    {
        Task HandleAsync(TEvent @event);
    }
}

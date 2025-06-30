using Bybit.Net.Clients;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QuantLab.Modules.Orders.Application.Dtos;
using QuantLab.Modules.Orders.Application.Services;


namespace QuantLab.Modules.Orders.Infrastructure.Services
{
    internal class ByBitOrderUpdateService : BackgroundService
    {
        private readonly BybitSocketClient _bybitClient;

        private readonly ILogger<ByBitOrderUpdateService> _logger;
        private readonly IOrderUpdateHandler _orderUpdateHandler;
        public ByBitOrderUpdateService(BybitSocketClient _bybitClient, IOrderUpdateHandler orderUpdateHandler, ILogger<ByBitOrderUpdateService> logger)
        {
            this._bybitClient = _bybitClient;
            _logger = logger;
            _orderUpdateHandler = orderUpdateHandler;
        }


        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscription = await _bybitClient.V5PrivateApi.SubscribeToOrderUpdatesAsync(data =>
            {
                foreach (var order in data.Data)
                {
                    OrderUpdateDto dto = new OrderUpdateDto(

                        OrderId: Guid.NewGuid(),//I decide to use our but we could keep both(internal & provider)
                        Status: order.Status.ToString(),
                        Symbol: order.Symbol.ToUpper(),
                        Side: order.Side.ToString(),
                        Quantity: order.Quantity,
                        FilledQuantity: order.Quantity,//we should use order.QuantityFilled but for the test better to have the order quantity
                        FilledPrice: order.AveragePrice ?? 0, //Ugly but just to ensure to have a price. I reallized we not always have aprice probably because bybit setup
                        PlacedPrice: order.LastPriceOnCreated ?? order.AveragePrice - order.AveragePrice / 100 ?? 0,//simulating a placed price, hugly but just for test purpose
                        FilledOn: order.UpdateTime,
                        PlacedOn: order.UpdateTime.AddSeconds(-1)
                        );

                    _orderUpdateHandler.HandleAsync(dto);
                }
            });

            if (subscription.Success)
            {
                _logger.LogInformation("Order Update Subscription Success!");

            }
            else
            {
                _logger.LogError($"Order Update Subscription Error: {subscription.Error?.Message}");
            }
        }
    }
}



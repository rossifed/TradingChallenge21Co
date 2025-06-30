using Bybit.Net.Clients;
using Bybit.Net.Enums;
using Bybit.Net.Objects.Models.V5;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using QuantLab.Modules.Orders.Application.Dtos;
using QuantLab.Modules.Orders.Application.Services;

namespace QuantLab.Modules.Orders.Infrastructure.Services
{
    internal class ByBitOrderPlacementService : IOrderPlacementService
    {
        private readonly BybitRestClient _bybitClient;

        private readonly ILogger<ByBitOrderPlacementService> _logger;

        public ByBitOrderPlacementService(BybitRestClient _bybitClient, ILogger<ByBitOrderPlacementService> logger)
        {
            this._bybitClient = _bybitClient;
            _logger = logger;
        }

        public async Task<string> PlaceOrderAsync(OrderDto order)
        {
            WebCallResult<BybitOrderId> result = await _bybitClient.V5Api.Trading.PlaceOrderAsync(
           clientOrderId: Guid.NewGuid().ToString(),
           category: Category.Spot,
           symbol: order.Symbol,
           side: order.Quantity > 0 ? OrderSide.Buy : OrderSide.Sell,
           type: NewOrderType.Market,
           quantity: Math.Abs(order.Quantity)


       );

            return result.Success ? "Sucess" : result.Error?.Message;

        }
    }

}

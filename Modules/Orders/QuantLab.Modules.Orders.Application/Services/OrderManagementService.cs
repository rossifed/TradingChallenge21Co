using Microsoft.Extensions.Logging;
using QuantLab.Modules.Orders.Application.Dtos;
using QuantLab.Modules.Orders.Application.Events.Out;
using QuantLab.Shared.Abstractions.Messaging;

namespace QuantLab.Modules.Orders.Application.Services
{
    internal interface IOrderManagementService
    {
        Task PlaceOrderAsync(OrderDto order);

        Task CreateOrder(string symbol, decimal quantity);


    }
    internal class OrderManagementService : IOrderManagementService, IOrderUpdateHandler
    {

        private readonly ILogger<OrderManagementService> _logger;
        private readonly IOrderPlacementService _orderPlacementService;
        private IMessageBroker _messageBroker;
        public OrderManagementService(ILogger<OrderManagementService> logger,
            IMessageBroker messageBroker, IOrderPlacementService orderPlacementService)
        {
            _logger = logger;
            this._orderPlacementService = orderPlacementService;
            this._messageBroker = messageBroker;
        }

        public async Task HandleAsync(OrderUpdateDto orderUpdateDto)
        {
            _logger.LogInformation($"Order Update: {orderUpdateDto}");
            if (orderUpdateDto.Status.ToUpper() == "FILLED")
            {


                TradeDto tradeDto = new TradeDto(
                    Guid.NewGuid(),
                    orderUpdateDto.OrderId,
                    orderUpdateDto.Symbol,
                    orderUpdateDto.Quantity,
                    orderUpdateDto.FilledQuantity,
                    orderUpdateDto.FilledPrice,
                    orderUpdateDto.PlacedPrice,
                    orderUpdateDto.PlacedOn,
                    orderUpdateDto.FilledOn);
                await _messageBroker.PublishAsync(new TradeExecuted(tradeDto));
            }
        }
        public async Task CreateOrder(string symbol, decimal quantity)
        {


            OrderDto newOrder = new OrderDto(Guid.NewGuid(), symbol, quantity);//simulating creation with an id
            _logger.LogInformation($"Order Received: {newOrder}");
            await _messageBroker.PublishAsync(new OrderReceived(newOrder));


        }
        public async Task PlaceOrderAsync(OrderDto order)
        {
            _logger.LogInformation($"Placing Order ...: {order}");


            string result = await _orderPlacementService.PlaceOrderAsync(order);
            _logger.LogInformation($"Placement {order} : {result}");

        }
    }
}

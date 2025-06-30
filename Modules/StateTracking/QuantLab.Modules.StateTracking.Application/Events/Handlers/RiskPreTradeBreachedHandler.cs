using Microsoft.Extensions.Logging;
using QuantLab.Modules.StateTracking.Application.Dtos;
using QuantLab.Modules.StateTracking.Application.Events.Out;
using QuantLab.Modules.StateTracking.Application.Mappers;
using QuantLab.Modules.StateTracking.Domain.Repositories;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.StateTracking.Application.Events.Handlers
{
    internal class RiskPreTradeBreachedHandler : IEventHandler<RiskPreTradeBreached>
    {

        private readonly IOrderRejectionRepository _orderRejectionRepository;
        private readonly ILogger<RiskPreTradeBreachedHandler> _logger;

        public RiskPreTradeBreachedHandler(IOrderRejectionRepository orderRejectionRepository, ILogger<RiskPreTradeBreachedHandler> logger)
        {
            _orderRejectionRepository = orderRejectionRepository;
            _logger = logger;
        }

        //simple log of rejected order as requested in the statetracking module
        public async Task HandleAsync(RiskPreTradeBreached @event, CancellationToken cancellationToken = default)
        {
            OrderDto orderDto = @event.Order;
            _logger.LogWarning($"Order: {orderDto.Id} Rejected");
            IEnumerable<ConstraintBreachDto> constraintBreaches = @event.ConstraintBreaches;
            foreach (ConstraintBreachDto constraintBreach in constraintBreaches)
            {
                _logger.LogWarning($"{constraintBreach.Message}");
            }
            await _orderRejectionRepository.AddAsync(orderDto.Map(@event.ConstraintBreaches));
            await Task.CompletedTask;
        }
    }
}

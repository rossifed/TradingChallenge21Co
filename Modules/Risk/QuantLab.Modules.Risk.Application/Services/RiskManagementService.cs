using Microsoft.Extensions.Logging;
using QuantLab.Modules.Risk.Application.Events.Out;
using QuantLab.Modules.Risk.Application.Mappers;
using QuantLab.Modules.Risk.Domain.Model;
using QuantLab.Modules.Risk.Domain.Model.Constraints;
using QuantLab.Modules.Risk.Domain.Services;
using QuantLab.Shared.Abstractions.Messaging;
namespace QuantLab.Modules.Risk.Application.Services
{
    internal interface IRiskManagementService
    {

        Task PerformRiskConstraintCheckAsync(WhatIfPosition whatIfPosition);
    }

    internal class RiskManagementService : IRiskManagementService
    {
        private readonly IRiskConstraintChecker _riskConstraintChecker;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<RiskManagementService> _logger;

        public RiskManagementService(IRiskConstraintChecker riskConstraintChecker,
            IMessageBroker messageBroker,
            ILogger<RiskManagementService> logger)
        {
            _riskConstraintChecker = riskConstraintChecker;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task PerformRiskConstraintCheckAsync(WhatIfPosition whatIfPosition)
        {
            try
            {
                ConstraintCheckResult result = _riskConstraintChecker.Check(whatIfPosition);
                var orderDto = whatIfPosition.Order.Map();
                if (result.IsBreached)
                {
                    await _messageBroker.PublishAsync(new RiskPreTradeBreached(orderDto, result.Breaches.Map()));
                    _logger.LogInformation($"Risk PreTrade Check Breached: Order:{whatIfPosition.Order}");
                }
                else
                {
                    await _messageBroker.PublishAsync(new RiskPreTradePassed(orderDto));
                    _logger.LogInformation($"Risk PreTrade Check Passed: Order:{whatIfPosition.Order}");
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"An Error Occured Performing Risk PreTrade Check: {whatIfPosition.Order}", ex);
            }
        }
    }
}

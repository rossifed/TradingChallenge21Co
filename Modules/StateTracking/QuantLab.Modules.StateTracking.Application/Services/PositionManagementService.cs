using Microsoft.Extensions.Logging;
using QuantLab.Modules.StateTracking.Application.Dtos;
using QuantLab.Modules.StateTracking.Application.Mappers;
using QuantLab.Modules.StateTracking.Domain.Model;
using QuantLab.Modules.StateTracking.Domain.Repositories;
using QuantLab.Shared.Abstractions.Messaging;

namespace QuantLab.Modules.StateTracking.Application.Services
{
    internal interface IPositionManagementService
    {
        Task ComputedWhatIfPositionAsync(Order order);
        Task UpdatePositionAsync(Trade trade);
        Task UpdatePositionAsync(BestBidOffer bestBidOffer);
    }

    internal class PositionManagementService : IPositionManagementService
    {
        private readonly IPositionAggregateRepository _positionAggregateRepository;
        private readonly IMessageBroker _messageBroker;

        private readonly IBestBidOfferRepository _bestBidOfferRepository;
        private readonly ILogger<PositionManagementService> _logger;

        public PositionManagementService(IPositionAggregateRepository positionAggregateRepository, IMessageBroker messageBroker, IBestBidOfferRepository bestBidOfferRepository, ILogger<PositionManagementService> logger)
        {
            _positionAggregateRepository = positionAggregateRepository;
            _messageBroker = messageBroker;
            _bestBidOfferRepository = bestBidOfferRepository;
            _logger = logger;
        }

        public async Task UpdatePositionAsync(Trade trade)
        {

            PositionAggregate? posAggregate = await _positionAggregateRepository.GetBySymbol(trade.Symbol);
            if (posAggregate == null)
            {
                posAggregate = PositionAggregate.CreateFromFirstTrade(trade);
                await _positionAggregateRepository.AddAsync(posAggregate);
                _logger.LogInformation($"Position Created: new:{posAggregate.Position}");
            }
            else
            {
                PositionAggregate updatedPosition = posAggregate.Update(trade);
                await _positionAggregateRepository.UpdateAsync(updatedPosition);
                _logger.LogInformation($"Position Updated: new:{updatedPosition.Position}");

            }
            await _messageBroker.PublishAsync(new PositionUpdated(posAggregate.Position.Map(), posAggregate.PositionPnL.Map()));

        }
        public async Task ComputedWhatIfPositionAsync(Order order)
        {

            BestBidOffer? lastBbo = await _bestBidOfferRepository.GetLasBySymbol(order.Symbol);

            if (lastBbo == null)
                _logger.LogWarning($"WhatIf Computation Abborted: No Best Bid Offer found for symbol: {order.Symbol}");
            else
            {
                Trade whatIfTrade = order.WhatIfTrade(lastBbo, WhatIfPricingScheme.MidPoint);//could be injected by config

                PositionAggregate? posAggregate = await _positionAggregateRepository.GetBySymbol(order.Symbol);
                if (posAggregate == null)
                {
                    posAggregate = PositionAggregate.CreateFromFirstTrade(whatIfTrade);
                }
                else
                {
                    posAggregate = posAggregate.Update(whatIfTrade);
                }
                _logger.LogInformation($"WhatIf Position generated: new:{posAggregate.Position} from Order {order}");
                await _messageBroker.PublishAsync(new WhatIfPositionComputed(order.Map(), posAggregate.Position.Map(), posAggregate.PositionPnL.Map()));

            }
        }



        public async Task UpdatePositionAsync(BestBidOffer bestBidOffer)
        {

            await _bestBidOfferRepository.AddAsync(bestBidOffer);
            PositionAggregate? posAggregate = await _positionAggregateRepository.GetBySymbol(bestBidOffer.Symbol);

            if (posAggregate != null)
            {

                PositionAggregate updatedPosition = posAggregate.Update(bestBidOffer.MidPrice);
                if (updatedPosition.PositionPnL.RealizedPnL != posAggregate.PositionPnL.RealizedPnL ||
                    updatedPosition.PositionPnL.UnrealizedPnL != posAggregate.PositionPnL.UnrealizedPnL ||
                        updatedPosition.PositionPnL.MarketPrice != posAggregate.PositionPnL.MarketPrice
                    )
                {
                    await _positionAggregateRepository.UpdateAsync(updatedPosition);
                    _logger.LogInformation($"Position Updated: new:{posAggregate.Position}");
                }

            }
        }
    }
}

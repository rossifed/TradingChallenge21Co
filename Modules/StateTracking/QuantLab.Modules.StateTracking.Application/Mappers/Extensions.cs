using QuantLab.Modules.StateTracking.Application.Dtos;
using QuantLab.Modules.StateTracking.Domain.Model;
namespace QuantLab.Modules.StateTracking.Application.Mappers
{
    internal static class Extensions
    {
        internal static OrderRejection Map(this OrderDto dto, IEnumerable<ConstraintBreachDto> constraintBreaches)
        {
            return new OrderRejection(dto.Map(), constraintBreaches.Select(x => x.Message));
        }
        internal static BestBidOffer Map(this BestBidOfferDto dto)
        {
            return new BestBidOffer(
                dto.Symbol,
                dto.BidPrice,
                dto.AskPrice,
                dto.BidSize,
                dto.AskSize,
                dto.MidPrice,
                dto.Spread,
                dto.TimeStampUtc
            );
        }
        internal static OrderDto Map(this Order order)
        {
            return new OrderDto(
                order.Id,
                order.Symbol,
                order.Quantity
            );
        }
        internal static Order Map(this OrderDto dto)
        {
            return new Order(
                dto.Id,
                dto.Symbol,
                dto.Quantity
            );
        }
        internal static Trade Map(this TradeDto dto)
        {
            return new Trade(
                dto.TradeId,
                dto.OrderId,
                dto.Symbol,
                dto.FilledQuantity,
                dto.ExecPrice,
                dto.FilledOn
            );
        }

        internal static PositionDto Map(this Position pos)
        {
            return new PositionDto(
               Id: pos.Id,
               Symbol: pos.Symbol,
               Quantity: pos.Quantity,
               EntryPrice: pos.AverageEntryPrice
            );
        }

        internal static PositionPnLDto Map(this PositionPnL pnL)
        {
            return new PositionPnLDto(
               PositionId: pnL.PositionId,
               UnrealizedPnL: pnL.UnrealizedPnL,
               RealizedPnL: pnL.RealizedPnL,
               MarketPrice: pnL.MarketPrice,
               DailyRealizedPnL: pnL.DailyRealizedPnL,
               DailyUnrealizedPnL: pnL.DailyUnralizedPnL,
               TotalDailyPnL: pnL.TotalDailyPnL,
               TotalPnL: pnL.TotalPnL,
               ComputedOnUtc: pnL.ComputedOn
            );
        }

    }
}

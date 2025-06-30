using QuantLab.Modules.StateTracking.Application.Dtos;
using QuantLab.Modules.StateTracking.Domain.Model;
namespace QuantLab.Modules.StateTracking.Infrastructure.Mappers
{
    internal static class Extensions
    {

        internal static Entities.Position Map(this Position pos)
        {
            Entities.Position entity = new Entities.Position(pos.Id, pos.Symbol, pos.Quantity, pos.AverageEntryPrice);
            return entity;
        }
        internal static Entities.Trade Map(this TradeDto dto)
        {
            return new Entities.Trade(
     dto.TradeId,
     dto.OrderId,
     dto.Symbol,
     dto.Quantity,
     dto.FilledQuantity,
     dto.ExecPrice,
     dto.PlacedPrice,
     dto.PlacedOn,
     dto.FilledOn
 );
        }
        internal static Entities.PositionPnL Map(this PositionPnL pnl)
        {
            Entities.PositionPnL entity = new Entities.PositionPnL
                (pnl.PositionId,
                pnl.UnrealizedPnL,
                pnl.RealizedPnL,
                 pnl.DailyUnralizedPnL,
                pnl.DailyRealizedPnL,
                pnl.TotalPnL,
                pnl.TotalDailyPnL,
                pnl.MarketPrice,
                pnl.ComputedOn);
            return entity;
        }

        internal static Position Map(this Entities.Position entity)
        {
            Position pos = new Position(entity.Id, entity.Symbol, entity.Quantity, entity.EntryPrice);

            return pos;
        }

        internal static PositionPnL Map(this Entities.PositionPnL entity)
        {
            PositionPnL pnl = new PositionPnL(
                entity.PositionId,
                entity.UnrealizedPnL,
                entity.RealizedPnL,
                entity.DailyUnrealizedPnL,
                entity.DailyRealizedPnL,
                entity.MarketPrice,
                entity.ComputedOn);

            return pnl;
        }
        internal static Entities.PositionAggregate Map(this PositionAggregate aggr)
        {
            Entities.PositionAggregate entity = new Entities.PositionAggregate(aggr.Position.Id, aggr.Position.Map(), aggr.PositionPnL.Map());

            return entity;
        }
        internal static PositionAggregate Map(this Entities.PositionAggregate entity)
        {
            PositionAggregate aggr = new PositionAggregate(entity.Position.Map(), entity.PositionPnL.Map());

            return aggr;
        }

        internal static Entities.BestBidOffer Map(this BestBidOffer bbo)
        {
            Entities.BestBidOffer entity = new Entities.BestBidOffer(bbo.Symbol,
                bbo.BidPrice,
                bbo.AskPrice,
                bbo.BidPrice,
                bbo.BidSize,
                bbo.MidPrice,
                bbo.Spread,
                bbo.TimeStampUtc);
            return entity;
        }

        internal static BestBidOffer Map(this Entities.BestBidOffer bbo)
        {
            return new BestBidOffer(bbo.Symbol,
                 bbo.BidPrice,
                 bbo.AskPrice,
                 bbo.BidSize,
                 bbo.AskSize,
                 bbo.MidPrice,
                 bbo.Spread,
                 bbo.TimeStampUtc);
        }

        internal static Order Map(this Entities.Order entity)
        {
            return new Order(entity.Id,
                 entity.Symbol,
                 entity.Quantity);
        }
        internal static Entities.Order Map(this Order entity)
        {
            return new Entities.Order(entity.Id,
                 entity.Symbol,
                 entity.Quantity);
        }
        internal static OrderRejection Map(this Entities.OrderRejection entity)
        {
            return new OrderRejection(entity.Order.Map(), entity.Reasons);
        }


        internal static Entities.OrderRejection Map(this OrderRejection entity)
        {
            return new Entities.OrderRejection(entity.Order.Map(), entity.Reasons);
        }
    }
}

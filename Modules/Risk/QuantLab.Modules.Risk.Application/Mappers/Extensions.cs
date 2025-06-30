using QuantLab.Modules.Risk.Application.Dtos;
using QuantLab.Modules.Risk.Domain.Model;
using QuantLab.Modules.Risk.Domain.Model.Constraints;

namespace QuantLab.Modules.Risk.Application.Mappers
{
    internal static class Extensions
    {
        internal static Order Map(this OrderDto dto)
        {
            return new Order(dto.Id,
                CryptoPair.Parse(dto.Symbol),
                dto.Quantity);
        }

        internal static OrderDto Map(this Order order)
        {
            return new OrderDto(order.Id,
               order.CryptoPair.ToString(),
                order.Quantity);
        }
        internal static Position Map(this PositionDto dto)
        {
            return new Position(dto.Id,
                CryptoPair.Parse(dto.Symbol),
                dto.Quantity,
                dto.EntryPrice);
        }
        internal static ConstraintBreachDto Map(this ConstraintBreach breach)
        {
            return new ConstraintBreachDto(breach.Message, breach.BrachedOn);
        }
        internal static IEnumerable<ConstraintBreachDto> Map(this IEnumerable<ConstraintBreach> breaches)
        {
            return breaches.Select(x => x.Map());
        }
        internal static PositionPnL Map(this PositionPnLDto dto)
        {
            return new PositionPnL(
          PositionId: dto.PositionId,
          UnrealizedPnL: dto.UnrealizedPnL,
          RealizedPnL: dto.RealizedPnL,
          DailyUnrealizedPnL: dto.DailyUnrealizedPnL,
          DailyRealizedPnL: dto.DailyRealizedPnL,
          TotalPnL: dto.TotalPnL,
          TotalDailyPnL: dto.TotalDailyPnL,
          MarketPrice: dto.MarketPrice,
          ComputedOnUtc: dto.ComputedOnUtc
      );
        }
    }
}


using QuantLab.Modules.Risk.Domain.Model;

namespace QuantLab.Modules.Risk.Infrastructure.Mappers
{
    internal static class Extensions
    {
        internal static Entities.Position Map(this Position pos)
        {
            return new Entities.Position(pos.Id,
                pos.CryptoPair.ToString(),
                pos.Quantity,
                pos.AverageEntryPrice);
        }

        internal static Position Map(this Entities.Position entity)
        {
            return new Position(entity.Id,
                CryptoPair.Parse(entity.Symbol),
                entity.Quantity,
                entity.EntryPrice);

        }
    }
}

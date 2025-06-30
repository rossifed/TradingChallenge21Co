namespace QuantLab.Modules.StateTracking.Domain.Model
{
    internal class PositionPnL
    {
        internal Guid PositionId { get; }

        internal decimal DailyUnralizedPnL { get; }
        internal decimal DailyRealizedPnL { get; }
        internal decimal UnrealizedPnL { get; }
        internal decimal RealizedPnL { get; }

        internal decimal MarketPrice { get; }
        internal DateTime ComputedOn { get; }

        internal decimal TotalPnL => UnrealizedPnL + RealizedPnL;
        internal decimal TotalDailyPnL => DailyUnralizedPnL + DailyRealizedPnL;
        // Pas de constructeur public - seule Position peut le créer
        internal PositionPnL(
            Guid positionId,
            decimal unrealizedPnL,
            decimal realizedPnL,
            decimal dailyUnrealizedPnL,
            decimal dailyRealizedPnL,
            decimal marketPrice,
            DateTime computedOn)
        {
            PositionId = positionId;
            UnrealizedPnL = unrealizedPnL;
            RealizedPnL = realizedPnL;
            ComputedOn = computedOn;
            MarketPrice = marketPrice;
        }
        internal static PositionPnL New(
            Guid positionId,
            decimal unrealizedPnL,
            decimal realizedPnL,
            decimal marketPrice)
        {
            var now = DateTime.UtcNow;
            // À la création, on considère que tout le PnL cumulatif est aussi le PnL du jour
            return new PositionPnL(
                positionId,
                unrealizedPnL,
                realizedPnL,
                dailyUnrealizedPnL: unrealizedPnL,
                dailyRealizedPnL: realizedPnL,
                marketPrice,
                computedOn: now
            );
        }
        internal PositionPnL Update(decimal unrealizedPnL, decimal realizedPnL, decimal marketPrice)
        {
            var now = DateTime.UtcNow;
            decimal dailyUnrealized;
            decimal dailyRealized;

            if (now.Date != this.ComputedOn.Date)
            {
                // New day we reset daily pnl
                dailyUnrealized = 0;
                dailyRealized = 0;
            }
            else
            {
                // computing intraday variation
                dailyUnrealized = unrealizedPnL - this.UnrealizedPnL;
                dailyRealized = realizedPnL - this.RealizedPnL;
            }

            return new PositionPnL(
                positionId: this.PositionId,
                unrealizedPnL: unrealizedPnL,
                realizedPnL: realizedPnL,
                dailyUnrealizedPnL: dailyUnrealized,
                dailyRealizedPnL: dailyRealized,
                marketPrice: marketPrice,
                computedOn: now
            );

        }

    }
}

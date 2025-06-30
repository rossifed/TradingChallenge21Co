namespace QuantLab.Modules.StateTracking.Domain.Model
{
    internal class PositionAggregate
    {
        internal Guid Id { get; }
        internal Position Position { get; }
        internal PositionPnL PositionPnL { get; }

        public PositionAggregate(Position position, PositionPnL valuation)
        {
            if (position.Id != valuation.PositionId)
                throw new ArgumentException("Position and PnL must have same ID");
            Id = position.Id;
            Position = position;
            PositionPnL = valuation;
        }


        internal static PositionAggregate CreateFromFirstTrade(Trade trade)
        {
            var position = Position.Create(trade);
            var valuation = PositionPnL.New(
                position.Id,
                position.ComputeUnrealizedPnL(trade.ExecPrice),
                0, // No realize PnL on the first trade
                trade.ExecPrice);

            return new PositionAggregate(position, valuation);
        }
        public PositionAggregate Update(Trade trade)
        {
            if (trade.Symbol != Position.Symbol)
                throw new ArgumentException("Position and Trade must have same Symbol");

            Position newPosition = Position.UpdatePosition(trade);


            decimal realizedPnL = Position.ComputeRealizedPnL(trade);
            decimal unrealizedPnl = newPosition.ComputeUnrealizedPnL(PositionPnL.MarketPrice);

            PositionPnL newPnL = PositionPnL.Update(
                unrealizedPnl,
                PositionPnL.RealizedPnL + realizedPnL,
                PositionPnL.MarketPrice);

            return new PositionAggregate(newPosition, newPnL);
        }

        public PositionAggregate Update(decimal marketPrice)
        {

            decimal unrealizedPnl = Position.ComputeUnrealizedPnL(marketPrice);
            PositionPnL newValuation = PositionPnL.Update(
                unrealizedPnl,
                PositionPnL.RealizedPnL,
                marketPrice);

            return new PositionAggregate(Position, newValuation);
        }

        public override string? ToString()
        {
            return $"{Position} ";
        }


        public override bool Equals(object? obj)
        {
            return obj is PositionAggregate aggregate &&
                   Id.Equals(aggregate.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}

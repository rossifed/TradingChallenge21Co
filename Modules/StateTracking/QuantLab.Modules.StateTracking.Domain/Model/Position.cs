namespace QuantLab.Modules.StateTracking.Domain.Model
{
    internal class Position
    {
        internal Guid Id { get; }
        internal string Symbol { get; }
        internal decimal Quantity { get; }
        internal decimal AverageEntryPrice { get; }
        internal decimal TotalCost { get; }




        public Position(Guid id, string symbol, decimal quantity, decimal entryPrice)
        {
            Id = id;
            Symbol = symbol;
            Quantity = quantity;
            AverageEntryPrice = entryPrice;
            TotalCost = quantity * entryPrice;

        }
        internal static Position CreateFlat(string symbol) => new Position(Guid.NewGuid(), symbol, 0, 0);

        internal static Position Create(Trade trade) => new Position(Guid.NewGuid(), trade.Symbol, trade.FilledQuantity, trade.ExecPrice);

        internal bool IsFlat => Quantity == 0;
        internal bool IsLong => Quantity > 0;
        internal bool IsShort => Quantity < 0;
        internal bool IsSameDirection(Trade trade)
            => (trade.IsLong && IsLong) || (trade.IsShort && IsShort);


        internal decimal ComputeAverageEntryPrice(Trade trade)
        {
            decimal newQuantity = Quantity + trade.FilledQuantity;
            decimal newAvgPrice = 0;
            decimal newTotalCost = 0;

            if (newQuantity == 0)//Close Position
            {

                newAvgPrice = 0;
                newTotalCost = 0;
            }
            else if (Quantity == 0)//New Position 
            {

                newAvgPrice = trade.ExecPrice;
                newTotalCost = newQuantity * trade.ExecPrice;
            }
            else if (IsSameDirection(trade))//Reinforcing positino
            {

                newTotalCost = TotalCost + (trade.FilledQuantity * trade.ExecPrice);
                newAvgPrice = newTotalCost / newQuantity;
            }
            else if (Math.Abs(trade.FilledQuantity) < Math.Abs(Quantity))//Reducing position
            {
                newAvgPrice = AverageEntryPrice;
                newTotalCost = newQuantity * newAvgPrice;
            }
            else// position inversion
            {

                decimal closingQuantity = -Quantity;
                decimal remainingQuantity = trade.FilledQuantity - closingQuantity;

                newAvgPrice = trade.ExecPrice;
                newTotalCost = newQuantity * newAvgPrice;
            }
            return newAvgPrice;
        }

        internal decimal ComputeNewQuantity(Trade trade)
        {
            return Quantity + trade.FilledQuantity;

        }

        internal Position UpdatePosition(Trade trade)
        {
            if (trade.Symbol != Symbol)
                throw new InvalidOperationException($"The Position {this} can't be updated with the trade {trade}");
            decimal newQuantity = ComputeNewQuantity(trade);
            decimal newAvgPrice = ComputeAverageEntryPrice(trade);

            return new Position(Id, Symbol, newQuantity, newAvgPrice);
        }


        internal decimal ComputeUnrealizedPnL(decimal marketPrice)
        {
            if (Quantity == 0) return 0; //closed pos
            return Quantity * (marketPrice - AverageEntryPrice);
        }
        internal bool IsReinforcingPosition(Trade trade)
        {
            if (Quantity == 0) return true;
            if (trade.FilledQuantity == 0) return true;
            return Math.Sign(Quantity) == Math.Sign(trade.FilledQuantity);
        }
        internal decimal ComputeRealizedPnL(Trade trade)
        {
            decimal closedQuantity = GetClosedQuantity(trade);
            if (closedQuantity == 0) return 0;

            return closedQuantity * Math.Sign(Quantity) * (trade.ExecPrice - AverageEntryPrice);
        }


        internal decimal GetClosedQuantity(Trade trade)
        {
            if (Quantity == 0) return 0; // Position fermée
            if (IsReinforcingPosition(trade)) return 0; // Renforcement

            return Math.Min(Math.Abs(trade.FilledQuantity), Math.Abs(Quantity));
        }
        public override string ToString()
        {
            return $"Symbol:{Symbol} Quantity:{Quantity} EntryPrice:{AverageEntryPrice}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   Id.Equals(position.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}


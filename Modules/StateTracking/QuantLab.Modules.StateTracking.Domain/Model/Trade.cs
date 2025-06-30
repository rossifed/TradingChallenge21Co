namespace QuantLab.Modules.StateTracking.Domain.Model
{
    internal class Trade
    {

        internal string Symbol { get; }

        internal decimal FilledQuantity { get; }
        internal decimal ExecPrice { get; }

        internal Guid TradeId { get; }
        internal Guid OrderId { get; }

        internal decimal TradeValue { get; }
        internal DateTime ExecutedOn { get; }
        internal string Side => IsLong ? "BUY" : "SELL";
        public Trade(
        Guid tradeId,
        Guid orderId,
        string symbol,
        decimal filledQuantity,
        decimal execPrice,
        DateTime executedOn)
        {
            TradeId = tradeId;
            OrderId = orderId;
            Symbol = symbol;
            ExecutedOn = executedOn;
            FilledQuantity = filledQuantity;
            ExecPrice = execPrice;
            TradeValue = filledQuantity * ExecPrice;
        }

        internal bool IsLong => FilledQuantity > 0;
        internal bool IsShort => FilledQuantity < 0;

        public override string? ToString()
        {
            return $"Trade:{Side} {FilledQuantity} {Symbol}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Trade trade &&
                   TradeId.Equals(trade.TradeId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TradeId);
        }
    }
}

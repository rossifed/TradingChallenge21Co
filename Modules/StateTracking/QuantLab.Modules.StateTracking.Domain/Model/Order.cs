namespace QuantLab.Modules.StateTracking.Domain.Model
{
    internal class Order
    {
        internal Guid Id { get; }
        internal string Symbol { get; }
        internal decimal Quantity { get; }
        internal bool IsBuy => Quantity > 0;

        internal string Side => IsBuy ? "BUY" : "SELL";
        public Order(Guid id, string symbol, decimal quantity)
        {
            Id = id;
            Symbol = symbol;
            Quantity = quantity;
        }

        internal Trade WhatIfTrade(BestBidOffer bestBidOffer, WhatIfPricingScheme scheme)
        {//Should have been done in the order module, but no time

            switch (scheme)
            {
                case WhatIfPricingScheme.Aggressive:
                    return new Trade(Guid.NewGuid(), Id, Symbol, Quantity, IsBuy ? bestBidOffer.AskPrice : bestBidOffer.BidPrice, DateTime.UtcNow);
                case WhatIfPricingScheme.Passive:
                    return new Trade(Guid.NewGuid(), Id, Symbol, Quantity, IsBuy ? bestBidOffer.BidPrice : bestBidOffer.AskPrice, DateTime.UtcNow);
                case WhatIfPricingScheme.MidPoint:
                    return new Trade(Guid.NewGuid(), Id, Symbol, Quantity, bestBidOffer.MidPrice, DateTime.UtcNow);


            }
            return new Trade(Guid.NewGuid(), Id, Symbol, Quantity, bestBidOffer.MidPrice, DateTime.UtcNow);
        }

        public override bool Equals(object? obj)
        {
            return obj is Order order &&
                   Id.Equals(order.Id) &&
                   Quantity == order.Quantity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Quantity);
        }

        public override string? ToString()
        {
            return $"Order:{Id} {Side} {Quantity}  {Symbol}";
        }

    }
}

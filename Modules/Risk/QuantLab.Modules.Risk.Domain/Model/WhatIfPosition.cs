namespace QuantLab.Modules.Risk.Domain.Model
{
    internal class WhatIfPosition
    {
        internal Order Order { get; }
        internal Position Position { get; }

        internal PositionPnL PnL { get; }

        public WhatIfPosition(Order order, Position position, PositionPnL pnL)
        {
            Order = order;
            Position = position;
            PnL = pnL;
        }
    }
}

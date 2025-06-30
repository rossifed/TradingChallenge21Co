using QuantLab.Modules.Risk.Domain.Model.Constraints;

namespace QuantLab.Modules.Risk.Domain.Model
{
    internal class RiskPreTradeCheck
    {
        internal Order Order { get; }

        internal WhatIfPosition WhatIfPosition { get; }

        IEnumerable<IRiskConstraint<Order>> OrderConstraints { get; }

        IEnumerable<IRiskConstraint<Position>> PositionConstraints { get; }

        IEnumerable<IRiskConstraint<PositionPnL>> PnLConstraints { get; }




    }
}

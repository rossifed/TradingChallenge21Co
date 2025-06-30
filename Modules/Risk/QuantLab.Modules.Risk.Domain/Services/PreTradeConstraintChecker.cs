using QuantLab.Modules.Risk.Domain.Model;
using QuantLab.Modules.Risk.Domain.Model.Constraints;

namespace QuantLab.Modules.Risk.Domain.Services
{
    internal class PreTradeConstraintChecker : IRiskConstraintChecker
    {
        private readonly IEnumerable<IRiskConstraint<Order>> _OrderConstraints;
        private readonly IEnumerable<IRiskConstraint<Position>> _PositionConstraints;

        private readonly IEnumerable<IRiskConstraint<PositionPnL>> _PnLConstraints;

        public PreTradeConstraintChecker(IEnumerable<IRiskConstraint<Order>> orderConstraints,
            IEnumerable<IRiskConstraint<Position>> positionConstraints,
            IEnumerable<IRiskConstraint<PositionPnL>> pnLConstraints)
        {
            _OrderConstraints = orderConstraints;
            _PositionConstraints = positionConstraints;
            _PnLConstraints = pnLConstraints;
        }

        public ConstraintCheckResult Check(WhatIfPosition whatIf)
        {

            var breaches = _PositionConstraints.SelectMany(x => x.Check(whatIf.Position))
                .Concat<ConstraintBreach>(_OrderConstraints.SelectMany(x => x.Check(whatIf.Order)))
                .Concat<ConstraintBreach>(_PnLConstraints.SelectMany(x => x.Check(whatIf.PnL)));
            return new ConstraintCheckResult(breaches);
        }


    }
}

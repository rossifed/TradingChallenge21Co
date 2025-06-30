namespace QuantLab.Modules.Risk.Domain.Model.Constraints
{
    internal class DailyLossLimitConstraint : IRiskConstraint<PositionPnL>
    {
        internal decimal LimitValue { get; }

        public DailyLossLimitConstraint(decimal limitValue)
        {
            LimitValue = limitValue;
        }

        public IEnumerable<ConstraintBreach> Check(PositionPnL pnl)
        {
            List<ConstraintBreach> breaches = new List<ConstraintBreach>();
            if (pnl.TotalDailyPnL < 0 && Math.Abs(LimitValue) < Math.Abs(pnl.TotalDailyPnL))
                breaches.Add(ConstraintBreach.Create($"Daily Loss Limit Constraint Beached: LimitValue:{LimitValue} PnL Size:{pnl.TotalDailyPnL}"));
            return breaches;
        }
    }
}

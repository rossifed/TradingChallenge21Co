namespace QuantLab.Modules.Risk.Domain.Model.Constraints
{
    internal class MaxTotalPositionConstraint : IRiskConstraint<Position>
    {
        internal decimal LimitValue { get; }
        internal Crypto Crypto { get; }
        public MaxTotalPositionConstraint(Crypto symbol, decimal limitValue)
        {
            Crypto = symbol;
            LimitValue = limitValue;
        }

        public IEnumerable<ConstraintBreach> Check(Position pos)
        {
            List<ConstraintBreach> breaches = new List<ConstraintBreach>();
            var limitValue = Math.Abs(LimitValue);
            var positionValue = Math.Abs(pos.Quantity);

            if (pos.CryptoPair.Contains(Crypto) && Math.Abs(pos.Quantity) > Math.Abs(LimitValue))
                breaches.Add(ConstraintBreach.Create($"Max Total Position Constraint Beached For {Crypto}: LimitValue:{LimitValue} Position Size:{pos.Quantity}"));
            return breaches;
        }
    }
}

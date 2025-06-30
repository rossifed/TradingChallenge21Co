namespace QuantLab.Modules.Risk.Domain.Model.Constraints
{
    internal class MaxOrderSizeConstraint : IRiskConstraint<Order>
    {
        internal decimal LimitValue { get; }

        public MaxOrderSizeConstraint(decimal limitValue)
        {
            LimitValue = limitValue;
        }

        public IEnumerable<ConstraintBreach> Check(Order t)
        {

            List<ConstraintBreach> breaches = new List<ConstraintBreach>();
            if (Math.Abs(LimitValue) < Math.Abs(t.Quantity))
                breaches.Add(ConstraintBreach.Create($"Max Order Size Constraint Beached: LimitValue:{LimitValue} Order Size:{t.Quantity}"));
            return breaches;
        }
    }
}

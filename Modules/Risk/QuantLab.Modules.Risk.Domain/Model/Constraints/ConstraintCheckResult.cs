namespace QuantLab.Modules.Risk.Domain.Model.Constraints
{
    internal class ConstraintCheckResult
    {

        internal IEnumerable<ConstraintBreach> Breaches { get; }

        internal bool IsBreached => Breaches.Any();

        internal ConstraintCheckResult() : this(Enumerable.Empty<ConstraintBreach>()) { }
        internal ConstraintCheckResult(IEnumerable<ConstraintBreach> breaches)
        {
            Breaches = breaches;
        }
        internal ConstraintCheckResult AddBreach(ConstraintBreach breach)
        {
            return new ConstraintCheckResult(Breaches.Append(breach));
        }

    }
}

namespace QuantLab.Modules.Risk.Domain.Model.Constraints
{
    internal interface IRiskConstraint<T>
    {

        IEnumerable<ConstraintBreach> Check(T t);
    }
}

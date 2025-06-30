namespace QuantLab.Modules.StateTracking.Domain.Model
{
    internal record OrderRejection(Order Order, IEnumerable<string> Reasons);
}

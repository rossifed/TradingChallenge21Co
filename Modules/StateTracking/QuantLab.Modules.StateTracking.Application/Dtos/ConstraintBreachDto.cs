namespace QuantLab.Modules.StateTracking.Application.Dtos
{
    public record ConstraintBreachDto(string Message, DateTime BreachedOnUtc);
}

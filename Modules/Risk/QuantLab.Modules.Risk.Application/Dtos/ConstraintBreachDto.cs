namespace QuantLab.Modules.Risk.Application.Dtos
{
    public record ConstraintBreachDto(string Message, DateTime BreachedOnUtc);
}

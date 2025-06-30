using QuantLab.Modules.StateTracking.Domain.Model;
namespace QuantLab.Modules.StateTracking.Domain.Repositories
{
    internal interface IPositionAggregateRepository
    {
        Task AddAsync(PositionAggregate pos);
        Task UpdateAsync(PositionAggregate pos);
        Task<PositionAggregate?> GetBySymbol(string symbol);
    }
}

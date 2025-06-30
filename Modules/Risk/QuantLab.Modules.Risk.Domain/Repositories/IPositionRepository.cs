using QuantLab.Modules.Risk.Domain.Model;

namespace QuantLab.Modules.Risk.Domain.Repositories
{
    internal interface IPositionRepository
    {
        Task<Position?> GetBySymbolAsync(string symbol);
        Task AddOrUpdate(Position position);
    }
}

using Microsoft.EntityFrameworkCore;
using QuantLab.Modules.Risk.Domain.Model;
using QuantLab.Modules.Risk.Domain.Repositories;
using QuantLab.Modules.Risk.Infrastructure.Mappers;
namespace QuantLab.Modules.Risk.Infrastructure.Repositories
{
    internal class PositionRepository : IPositionRepository
    {
        private readonly Entities.RiskDbContext _context;

        public async Task<Position?> GetBySymbolAsync(string symbol)
        {
            var entity = await _context.Positions.AsNoTracking()
                  .SingleOrDefaultAsync(x => x.Symbol == symbol);
            return entity?.Map();
        }
        public async Task AddOrUpdate(Position position)
        {
            _context.Upsert<Entities.Position>(position.Map());
            await _context.SaveChangesAsync();
        }
    }
}

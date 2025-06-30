using Microsoft.EntityFrameworkCore;
using QuantLab.Modules.StateTracking.Domain.Model;
using QuantLab.Modules.StateTracking.Domain.Repositories;
using QuantLab.Modules.StateTracking.Infrastructure.Mappers;
namespace QuantLab.Modules.StateTracking.Infrastructure.Repositories
{

    internal class PositionAggregateRepository : IPositionAggregateRepository
    {


        private readonly Entities.StateTrackingDbContext _context;

        public PositionAggregateRepository(Entities.StateTrackingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PositionAggregate aggr)
        {
            _context.ChangeTracker.Clear();
            await _context.AddAsync<Entities.Position>(aggr.Position.Map());


            await _context.AddAsync<Entities.PositionPnL>(aggr.PositionPnL.Map());
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(PositionAggregate aggr)
        {
            _context.ChangeTracker.Clear();
            _context.Update<Entities.Position>(aggr.Position.Map());

            _context.Update<Entities.PositionPnL>(aggr.PositionPnL.Map());
            await _context.SaveChangesAsync();

        }

        public async Task<PositionAggregate?> GetBySymbol(string symbol)
        {
            var pos = await _context.Positions.AsNoTracking()
                              .SingleOrDefaultAsync(x => x.Symbol == symbol);
            if (pos == null) return null;
            var pnl = await _context.PositionPnLs.AsNoTracking()
                                    .SingleOrDefaultAsync(x => x.PositionId == pos.Id);
            return (pnl == null)
                ? null
                : new PositionAggregate(pos.Map(), pnl.Map());
        }
    }
}

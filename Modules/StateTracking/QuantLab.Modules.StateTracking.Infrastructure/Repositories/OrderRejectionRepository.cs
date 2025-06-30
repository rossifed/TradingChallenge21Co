using Microsoft.EntityFrameworkCore;
using QuantLab.Modules.StateTracking.Domain.Model;
using QuantLab.Modules.StateTracking.Domain.Repositories;
using QuantLab.Modules.StateTracking.Infrastructure.Mappers;

namespace QuantLab.Modules.StateTracking.Infrastructure.Repositories
{

    internal class OrderRejectionRepository : IOrderRejectionRepository
    {


        private readonly Entities.StateTrackingDbContext _context;

        public OrderRejectionRepository(Entities.StateTrackingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderRejection order)
        {
            await _context.AddAsync(order.Map());
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderRejection>> GetAllBySymbol(string symbol)
        {
            var entities = await _context.OrderRejections
                 .Where(x => x.Order.Symbol == symbol)
                 .ToListAsync();
            return entities.Select(x => x.Map()).ToList();
        }
    }
}

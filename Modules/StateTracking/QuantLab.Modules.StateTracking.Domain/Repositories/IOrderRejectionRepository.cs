using QuantLab.Modules.StateTracking.Domain.Model;
namespace QuantLab.Modules.StateTracking.Domain.Repositories
{
    internal interface IOrderRejectionRepository
    {
        Task AddAsync(OrderRejection pos);

        Task<IEnumerable<OrderRejection>> GetAllBySymbol(string symbol);
    }
}

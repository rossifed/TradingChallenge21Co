using QuantLab.Modules.StateTracking.Domain.Model;
namespace QuantLab.Modules.StateTracking.Domain.Repositories
{
    internal interface IBestBidOfferRepository
    {
        Task AddAsync(BestBidOffer bbo);
        Task<BestBidOffer?> GetLasBySymbol(string symbol);
    }
}

using QuantLab.Modules.StateTracking.Domain.Model;
using QuantLab.Modules.StateTracking.Domain.Repositories;
using System.Collections.Concurrent;

namespace QuantLab.Modules.StateTracking.Infrastructure.Repositories
{

    //I use a simple dummy caching approach to avoid EF complexity and perf impact
    internal class BestBidOfferRepository : IBestBidOfferRepository
    {
        private readonly ConcurrentDictionary<string, Queue<BestBidOffer>> _bboCache = new();

        public Task AddAsync(BestBidOffer bbo)
        {
            var queue = _bboCache.GetOrAdd(bbo.Symbol, _ => new Queue<BestBidOffer>());
            lock (queue) // Queue is not thread-safe, so lock per symbol
            {
                queue.Enqueue(bbo);
                if (queue.Count > 20)//keep the last 20 but should be injected by config
                    queue.Dequeue();
            }
            return Task.CompletedTask;
        }

        public Task<BestBidOffer?> GetLasBySymbol(string symbol)
        {
            if (_bboCache.TryGetValue(symbol, out var queue))
            {
                lock (queue)
                {
                    return Task.FromResult(queue.LastOrDefault());
                }
            }
            return Task.FromResult<BestBidOffer?>(null);
        }
    }
}

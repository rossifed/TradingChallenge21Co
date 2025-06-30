using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.MarketData.Application.Dtos;
using QuantLab.Modules.MarketData.Application.Services;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("QuantLab.Modules.MarketData.Infrastructure")]
[assembly: InternalsVisibleTo("QuantLab.Modules.MarketData.Api")]
namespace QuantLab.Modules.MarketData.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services.AddServices();

        }
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddSingleton<IMarketDataEventHandler<BestBidOfferDto>, BestBidOfferEventHandler>();

        }
    }
}

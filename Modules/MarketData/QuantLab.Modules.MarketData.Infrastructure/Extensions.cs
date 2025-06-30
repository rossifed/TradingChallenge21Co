using Bybit.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuantLab.Modules.MarketData.Application.Services;
using QuantLab.Modules.MarketData.Infrastructure.Services;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("QuantLab.Modules.MarketData.Api")]
namespace QuantLab.Modules.MarketData.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddDaos().AddRepositories().AddServices();

        }

        public static IServiceCollection AddDaos(this IServiceCollection services)
        {
            return services;

        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;

        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddSingleton(sp =>
            {
                // on configure le client WebSocket ici
                return new BybitSocketClient(options =>
                {
                    options.ReconnectPolicy = ReconnectPolicy.FixedDelay;
                    options.ReconnectInterval = TimeSpan.FromSeconds(5);
                }
                    );
            });
            return services
    .AddSingleton<ByBitBestBidOfferService>()
    .AddSingleton<IHostedService>(sp => sp.GetRequiredService<ByBitBestBidOfferService>())
    .AddSingleton<IBestBidOfferService>(sp => sp.GetRequiredService<ByBitBestBidOfferService>());

        }

    }
}

using Bybit.Net;
using Bybit.Net.Clients;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.Orders.Application.Services;
using QuantLab.Modules.Orders.Infrastructure.Services;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("QuantLab.Modules.Orders.Api")]
namespace QuantLab.Modules.Orders.Infrastructure
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
                    options.ApiCredentials = new ApiCredentials("WtZmNA03iIJPEk2MpJ", "GNBwFMyMYEMEba6OIWVI11oNXQnnIkg77nfA");
                    options.Environment = BybitEnvironment.Testnet;
                });

            });

            services.AddSingleton(sp =>
            {
                // on configure le client WebSocket ici
                return new BybitRestClient(options =>
                {
                    options.ApiCredentials = new ApiCredentials("WtZmNA03iIJPEk2MpJ", "GNBwFMyMYEMEba6OIWVI11oNXQnnIkg77nfA");
                    options.Environment = BybitEnvironment.Testnet;
                });

            });
            services.AddSingleton<IOrderPlacementService, ByBitOrderPlacementService>();

            return services.AddHostedService<ByBitOrderUpdateService>();

        }

    }
}

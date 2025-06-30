using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("QuantLab.Modules.MarketData.Application")]
[assembly: InternalsVisibleTo("QuantLab.Modules.MarketData.Infrastructure")]
[assembly: InternalsVisibleTo("QuantLab.Modules.MarketData.Api")]
[assembly: InternalsVisibleTo("QuantLab.Modules.MarketData.Domain.Tests")]
namespace QuantLab.Modules.MarketData.Domain
{
    public static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services.AddServices();

        }
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;

        }
    }

}


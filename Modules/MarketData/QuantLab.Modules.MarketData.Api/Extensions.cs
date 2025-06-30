using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.MarketData.Application;
using QuantLab.Modules.MarketData.Domain;
using QuantLab.Modules.MarketData.Infrastructure;

namespace QuantLab.Modules.MarketData.Api;

internal static class Extensions
{
    public static IServiceCollection AddModule(this IServiceCollection services)
    {
        return services.AddDomain().AddApplication().AddInfrastructure();

    }
}
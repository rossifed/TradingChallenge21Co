using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.Orders.Application;
using QuantLab.Modules.Orders.Domain;
using QuantLab.Modules.Orders.Infrastructure;

namespace QuantLab.Modules.Orders.Api;

internal static class Extensions
{
    public static IServiceCollection AddModule(this IServiceCollection services)
    {
        return services.AddDomain().AddInfrastructure().AddApplication();

    }
}
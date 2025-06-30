using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.Risk.Application;
using QuantLab.Modules.Risk.Domain;
using QuantLab.Modules.Risk.Infrastructure;

namespace QuantLab.Modules.Risk.Api;

internal static class Extensions
{
    public static IServiceCollection AddModule(this IServiceCollection services)
    {
        return services.AddDomain().AddInfrastructure().AddApplication();

    }
}
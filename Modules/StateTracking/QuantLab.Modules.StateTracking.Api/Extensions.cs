using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.StateTracking.Application;
using QuantLab.Modules.StateTracking.Domain;
using QuantLab.Modules.StateTracking.Infrastructure;

namespace QuantLab.Modules.StateTracking.Api;

internal static class Extensions
{
    public static IServiceCollection AddModule(this IServiceCollection services)
    {
        return services.AddDomain().AddInfrastructure().AddApplication();

    }
}
using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.StateTracking.Application.Services;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("QuantLab.Modules.StateTracking.Infrastructure")]
[assembly: InternalsVisibleTo("QuantLab.Modules.StateTracking.Api")]
namespace QuantLab.Modules.StateTracking.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services.AddServices();

        }
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<IPositionManagementService, PositionManagementService>();

        }
    }
}

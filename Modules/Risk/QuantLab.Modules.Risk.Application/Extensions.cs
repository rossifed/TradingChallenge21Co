using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.Risk.Application.Services;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("QuantLab.Modules.Risk.Infrastructure")]
[assembly: InternalsVisibleTo("QuantLab.Modules.Risk.Api")]
namespace QuantLab.Modules.Risk.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services.AddServices();

        }
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<IRiskManagementService, RiskManagementService>();

        }
    }
}

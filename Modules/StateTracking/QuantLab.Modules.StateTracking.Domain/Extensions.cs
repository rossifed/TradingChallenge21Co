using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("QuantLab.Modules.StateTracking.Application")]
[assembly: InternalsVisibleTo("QuantLab.Modules.StateTracking.Infrastructure")]
[assembly: InternalsVisibleTo("QuantLab.Modules.StateTracking.Api")]
[assembly: InternalsVisibleTo("QuantLab.Modules.StateTracking.Domain.Tests")]
namespace QuantLab.Modules.StateTracking.Domain
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


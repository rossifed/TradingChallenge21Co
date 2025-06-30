using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("QuantLab.Modules.Orders.Application")]
[assembly: InternalsVisibleTo("QuantLab.Modules.Orders.Infrastructure")]
[assembly: InternalsVisibleTo("QuantLab.Modules.Orders.Api")]
[assembly: InternalsVisibleTo("QuantLab.Modules.Orders.Domain.Tests")]
namespace QuantLab.Modules.Orders.Domain
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


using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.Risk.Domain.Repositories;
using QuantLab.Modules.Risk.Infrastructure.Repositories;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("QuantLab.Modules.Risk.Api")]
namespace QuantLab.Modules.Risk.Infrastructure
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
            return services.AddScoped<IPositionRepository, PositionRepository>();

        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;

        }

    }
}

using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.StateTracking.Application.Services;
using QuantLab.Modules.StateTracking.Domain.Repositories;
using QuantLab.Modules.StateTracking.Infrastructure.Entities;
using QuantLab.Modules.StateTracking.Infrastructure.Repositories;
using QuantLab.Modules.StateTracking.Infrastructure.Services;
using QuantLab.Shared.Infrastructure.Database;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("QuantLab.Modules.StateTracking.Api")]
namespace QuantLab.Modules.StateTracking.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddDatabase<StateTrackingDbContext>().AddDaos().AddRepositories().AddServices();

        }

        public static IServiceCollection AddDaos(this IServiceCollection services)
        {
            return services;

        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IPositionAggregateRepository, PositionAggregateRepository>()
                .AddSingleton<IBestBidOfferRepository, BestBidOfferRepository>()
                .AddScoped<IOrderRejectionRepository, OrderRejectionRepository>();

        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ITradeCaptureService, TradeCaptureService>();

        }

    }
}

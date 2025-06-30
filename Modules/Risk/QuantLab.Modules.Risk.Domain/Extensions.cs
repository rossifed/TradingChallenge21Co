using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.Risk.Domain.Model;
using QuantLab.Modules.Risk.Domain.Model.Constraints;
using QuantLab.Modules.Risk.Domain.Services;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("QuantLab.Modules.Risk.Application")]
[assembly: InternalsVisibleTo("QuantLab.Modules.Risk.Infrastructure")]
[assembly: InternalsVisibleTo("QuantLab.Modules.Risk.Api")]
[assembly: InternalsVisibleTo("QuantLab.Modules.Risk.Domain.Test")]
namespace QuantLab.Modules.Risk.Domain
{
    public static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services.AddServices();

        }
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Register specific risk constraints
            // (hardcoded for now; could be driven by config or DB later)
            services.AddScoped<IRiskConstraint<Position>>(provider =>
                new MaxTotalPositionConstraint(Crypto.BTC, limitValue: 5));

            services.AddScoped<IRiskConstraint<PositionPnL>>(provider =>
                new DailyLossLimitConstraint(limitValue: 10000));

            services.AddScoped<IRiskConstraint<Order>>(provider =>
                new MaxOrderSizeConstraint(limitValue: 50000m));


            services.AddScoped<IRiskConstraintChecker, PreTradeConstraintChecker>();

            return services;

        }
    }

}


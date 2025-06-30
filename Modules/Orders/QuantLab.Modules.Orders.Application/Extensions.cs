using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.Orders.Application.Services;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("QuantLab.Modules.Orders.Infrastructure")]
[assembly: InternalsVisibleTo("QuantLab.Modules.Orders.Api")]
namespace QuantLab.Modules.Orders.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services.AddServices();

        }
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<OrderManagementService>();

            // 2. Enregistrer les interfaces qui pointent vers la même instance
            services.AddSingleton<IOrderManagementService>(provider =>
                provider.GetRequiredService<OrderManagementService>());

            services.AddSingleton<IOrderUpdateHandler>(provider =>
                provider.GetRequiredService<OrderManagementService>());
            return services;

        }
    }
}

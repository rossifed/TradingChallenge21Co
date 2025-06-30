using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.Orders.Api;
using QuantLab.Shared.Abstractions.Modules;
namespace Orders.Api
{
    internal class OrdersModule : IModule
    {
        public string Name => "Orders";

        public void Register(IServiceCollection services)
        {
            services.AddModule();
        }
    }
}

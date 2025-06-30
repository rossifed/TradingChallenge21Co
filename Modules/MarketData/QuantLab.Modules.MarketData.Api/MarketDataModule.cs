using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.MarketData.Api;
using QuantLab.Shared.Abstractions.Modules;
namespace MarketData.Api
{
    internal class MarketDataModule : IModule
    {
        public string Name { get; } = "MarketData";

        public void Register(IServiceCollection services)
        {
            services.AddModule();
        }
    }
}

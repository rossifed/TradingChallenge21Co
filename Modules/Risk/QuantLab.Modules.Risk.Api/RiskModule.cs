using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.Risk.Api;
using QuantLab.Shared.Abstractions.Modules;

namespace Risk.Api
{
    internal class RiskModule : IModule
    {
        public string Name => "Risk";

        public void Register(IServiceCollection services)
        {
            services.AddModule();
        }
    }
}

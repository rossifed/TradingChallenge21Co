using Microsoft.Extensions.DependencyInjection;
using QuantLab.Modules.StateTracking.Api;
using QuantLab.Shared.Abstractions.Modules;

namespace StateTracking.Api
{
    internal class StateTrackingModule : IModule
    {
        public string Name => "StateTracking";

        public void Register(IServiceCollection services)
        {
            services.AddModule();
        }
    }
}

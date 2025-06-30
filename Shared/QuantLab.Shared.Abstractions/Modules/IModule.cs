using Microsoft.Extensions.DependencyInjection;

namespace QuantLab.Shared.Abstractions.Modules
{
    public interface IModule
    {
        IEnumerable<string> Policies => null;
        string Name { get; }
        void Register(IServiceCollection services);
        //void Use(IApplicationBuilder app);

        //void Configure(IHostBuilder hostBuilder);


    }
}

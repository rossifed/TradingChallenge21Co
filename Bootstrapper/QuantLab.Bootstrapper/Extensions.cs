using QuantLab.Shared.Abstractions.Modules;
using QuantLab.Shared.Infrastructure;
using QuantLab.Shared.Infrastructure.Modules;

namespace QuantLab.Bootstrapper
{
    internal static class Extensions
    {

        public static IEnumerable<IModule> RegisterModules(this IServiceCollection services, IConfiguration configuration)
        {
            var assemblies = ModuleLoader.LoadAssemblies("QuantLab.Modules.", configuration);
            var modules = ModuleLoader.LoadModules(assemblies);

            services.AddModularInfrastructure(configuration, assemblies, modules);
            modules.ToList().ForEach(module => module.Register(services));
            return modules;
        }
    }
}

using Microsoft.Extensions.Configuration;
using QuantLab.Shared.Abstractions.Modules;
using System.Reflection;

namespace QuantLab.Shared.Infrastructure.Modules
{
    public class ModuleLoader
    {
        private static List<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }
        private static List<string> GetLocations(List<Assembly> assemblies)
        {
            return assemblies.Where(assembly => !assembly.IsDynamic).Select(assembly => assembly.Location).ToList();
        }

        private static string GetModuleName(string file, string modulesNamespace)
        {
            return file.Split(modulesNamespace)[1].Split(".")[0].ToLowerInvariant();
        }
        private static List<string> GetModuleFiles(List<Assembly> assemblies)
        {
            var locations = GetLocations(assemblies);
            return Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(file => !locations.Contains(file, StringComparer.InvariantCultureIgnoreCase))
                .ToList();
        }
        private static bool IsEnabled(string moduleName, IConfiguration configuration)
        {

            return configuration.GetValue<bool>($"{moduleName}:module:enabled");
        }
        public static IList<Assembly> LoadAssemblies(string modulesNamespace, IConfiguration configuration)
        {

            var assemblies = GetAssemblies();

            var moduleFiles = GetModuleFiles(assemblies);

            List<string> enabledModuleFiles = GetModuleFiles(assemblies)
                .Where(file => file.Contains(modulesNamespace) && IsEnabled(GetModuleName(file, modulesNamespace), configuration))
                .ToList();

            enabledModuleFiles.ForEach(file => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(file))));

            return assemblies;
        }

        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
         => assemblies.SelectMany(assembly => assembly.GetTypes())
             .Where(@type => typeof(IModule).IsAssignableFrom(@type) && !@type.IsInterface)
             .OrderBy(@type => type.Name)
             .Select(Activator.CreateInstance)
             .Cast<IModule>()
             .ToList();

    }
}

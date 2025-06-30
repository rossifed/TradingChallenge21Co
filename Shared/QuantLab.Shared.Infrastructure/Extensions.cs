using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuantLab.Shared.Abstractions.Dispatchers;
using QuantLab.Shared.Abstractions.Modules;
using QuantLab.Shared.Infrastructure.Api;
using QuantLab.Shared.Infrastructure.Commands;
using QuantLab.Shared.Infrastructure.Dispatchers;
using QuantLab.Shared.Infrastructure.Messaging;
using QuantLab.Shared.Infrastructure.Modules;
using QuantLab.Shared.Infrastructure.Queries;
using QuantumQube.Shared.Infrastructure.Events;
using System.Reflection;
using System.Text.Json.Serialization;

namespace QuantLab.Shared.Infrastructure
{
    public static class Extensions
    {

        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services,
            IConfiguration config,
            IList<Assembly> assemblies,
            IList<IModule> modules)
        {

            var disabledModules = new List<string>();
            using var scope = services.BuildServiceProvider().CreateScope();
            {

                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                foreach (var (key, value) in configuration.AsEnumerable())
                {
                    if (!key.Contains(":module:enabled"))
                    {
                        continue;
                    }
                    if (!bool.Parse(value))
                    {
                        disabledModules.Add(key.Split(":")[0]);
                    }
                }
            }
            services
               .AddMemoryCache()

             .AddModuleInfo(modules)
             .AddModuleRequests(assemblies)
            // .AddModuleGuiConnectors(moduleGuiConnectors)
             .AddCommands(assemblies)
             .AddQueries(assemblies)
             .AddEvents(assemblies)
             //.AddCaching(assemblies)
             .AddSingleton<IDispatcher, InMemoryDispatcher>()
             .AddMessaging()
             //.AddDatabaseOptions()
             //.UseQuartz(config)
             .AddControllers().AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
             })
             .ConfigureApplicationPartManager(manager =>
             {

                 var removedParts = new List<ApplicationPart>();
                 foreach (var diabledModule in disabledModules)
                 {

                     var parts = manager.ApplicationParts.Where(x => x.Name.Contains(diabledModule, StringComparison.InvariantCultureIgnoreCase));
                     removedParts.AddRange(parts);
                 }
                 foreach (var part in removedParts)
                 {

                     manager.ApplicationParts.Remove(part);
                 }
                 manager.FeatureProviders.Add(new InternalControllerFreatureProvider());
             });


            return services;
        }
        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        public static string GetModuleName(this object value)
          => value?.GetType().GetModuleName() ?? string.Empty;
        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }


        public static string GetModuleName(this Type type, string namespacePart = "Modules", int splitIndex = 2)
        {
            if (type?.Namespace is null)
            {
                return string.Empty;
            }

            return type.Namespace.Contains(namespacePart)
                ? type.Namespace.Split(".")[splitIndex].ToLowerInvariant()
                : string.Empty;
        }
    }
}

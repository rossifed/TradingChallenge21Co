using Microsoft.Extensions.DependencyInjection;
using QuantLab.Shared.Abstractions.Commands;
using System.Reflection;

namespace QuantLab.Shared.Infrastructure.Commands
{
    public static class Extensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }
    }
}

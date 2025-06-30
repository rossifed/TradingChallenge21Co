using Microsoft.Extensions.DependencyInjection;
using QuantLab.Shared.Abstractions.Events;
using QuantLab.Shared.Infrastructure.Events;
using System.Reflection;
namespace QuantumQube.Shared.Infrastructure.Events
{
    internal static class Extensions
    {

        public static IServiceCollection AddEvents(this IServiceCollection services, IList<Assembly> assemblies)
        {

            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.Scan(service => service.FromAssemblies(assemblies)
            .AddClasses(@class => @class.AssignableTo(typeof(IEventHandler<>)), false)
            // .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
            return services;
        }
    }
}

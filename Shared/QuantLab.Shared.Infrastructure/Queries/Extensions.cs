using Microsoft.Extensions.DependencyInjection;
using QuantLab.Shared.Abstractions.Queries;
using System.Reflection;
namespace QuantLab.Shared.Infrastructure.Queries
{
    public static class Extensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }
    }
}

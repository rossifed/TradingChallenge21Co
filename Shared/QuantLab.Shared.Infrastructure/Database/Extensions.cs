//using Microsoft.EntityFrameworkCore.sq;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;
namespace QuantLab.Shared.Infrastructure.Database
{
    public static class Extensions
    {

        public static IServiceCollection AddDatabaseOptions(this IServiceCollection services)
        {
            var options = services.GetOptions<DatabaseOptions>("database");
            services.AddSingleton(options);
            services.AddSingleton(new UnitOfWorkRegistry());

            return services;
        }

        public static IServiceCollection AddDatabase<T>(this IServiceCollection services) where T : DbContext
        {
            var options = services.GetOptions<DatabaseOptions>("database");
            services.AddDbContext<T>(x =>
            {
                x.UseInMemoryDatabase(options.ConnectionString);
                x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
                );

            // services.AddSingleton(new UnitOfWorkTypeRegistry());

            return services;
        }

        public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : class, IUnitOfWork
        {
            services.AddScoped<IUnitOfWork, T>();
            services.AddScoped<T>();
            using var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<UnitOfWorkRegistry>().Register<T>();

            return services;
        }
    }
}

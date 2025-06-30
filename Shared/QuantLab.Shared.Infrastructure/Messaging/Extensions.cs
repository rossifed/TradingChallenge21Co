using Microsoft.Extensions.DependencyInjection;
using QuantLab.Shared.Abstractions.Messaging;
using QuantLab.Shared.Infrastructure.Messaging.Contexts;

namespace QuantLab.Shared.Infrastructure.Messaging
{
    internal static class Extensions
    {

        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {

            services.AddTransient<IMessageBroker, InMemoryMessageBroker>();
            services.AddSingleton<IMessageChannel, MessageChannel>();
            services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();
            services.AddSingleton<IMessageContextProvider, MessageContextProvider>();
            services.AddSingleton<IMessageContextRegistry, MessageContextRegistry>();
            var messagingOptions = services.GetOptions<MessagingOptions>(sectionName: "messaging");
            services.AddSingleton(messagingOptions);

            services.AddHostedService<AsyncDispatcherJob>();

            return services;

        }
    }
}

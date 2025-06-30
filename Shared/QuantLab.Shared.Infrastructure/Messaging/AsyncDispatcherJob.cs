using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QuantLab.Shared.Abstractions.Modules;
namespace QuantLab.Shared.Infrastructure.Messaging
{
    internal class AsyncDispatcherJob : BackgroundService
    {
        private IMessageChannel MessageChannel { get; }
        private IModuleClient ModuleClient { get; }
        private ILogger<AsyncDispatcherJob> Logger { get; }
        public AsyncDispatcherJob(IMessageChannel messageChannel, IModuleClient moduleClient, ILogger<AsyncDispatcherJob> logger)
        {
            this.MessageChannel = messageChannel;
            this.Logger = logger;
            this.ModuleClient = moduleClient;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var envelope in MessageChannel.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await ModuleClient.PublishAsync(envelope.message, stoppingToken);
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception, exception.Message);
                }

            }
        }
    }
}

using Dapr.Client;
using Microsoft.Extensions.Logging;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;

namespace Mover.Shared;

public class ServiceBus : IServiceBus
{
    private readonly DaprClient dapr;
    private readonly ILogger logger;

    public ServiceBus(DaprClient dapr, ILoggerFactory loggerFactory)
    {
        this.dapr = dapr;
        this.logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken) where T : Event
    {
        var topicName = @event.GetType().Name;

        logger.LogInformation(
            "Publishing event {@Event} to {PubsubName}.{TopicName}",
            @event,
            Constants.Dapr.MOVER_PUBSUB,
            topicName);

        // We need to make sure that we pass the concrete type to PublishEventAsync,
        // which can be accomplished by casting the event to dynamic. This ensures
        // that all event fields are properly serialized.
        await dapr.PublishEventAsync(Constants.Dapr.MOVER_PUBSUB, topicName, (object)@event, cancellationToken).ConfigureAwait(false);
    }

    public async Task SendAsync<T>(T command, CancellationToken cancellationToken) where T : Command
    {
        var topicName = command.Name;

        logger.LogInformation(
            "Send command {Command} to {PubsubName}.{TopicName}",
            command,
            Constants.Dapr.MOVER_PUBSUB,
            topicName);

        // We need to make sure that we pass the concrete type to PublishEventAsync,
        // which can be accomplished by casting the event to dynamic. This ensures
        // that all event fields are properly serialized.
        await dapr.PublishEventAsync(Constants.Dapr.MOVER_PUBSUB, topicName, (object)command, cancellationToken).ConfigureAwait(false);
    }
}

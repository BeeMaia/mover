using Dapr;
using Mover.Shared;
using Mover.Shared.Dispatchers;
using Mover.Shared.Interfaces;
using Mover.Stats.Shared.Commands;
using Mover.Uploader.Shared.Events;

namespace Mover.Stats;

public static class StatsDispatcher
{
    [Topic(Constants.Dapr.MOVER_PUBSUB, UploadedGpx.Topic)]
    public static Task<IResult> HandleUploadedGpxAsync(UploadedGpx @event, IEventHandler<UploadedGpx> eventHandler, CancellationToken cancellationToken)
    {
        return EventDispatcher.DispatchAsync(@event, eventHandler, cancellationToken);
    }

    [Topic(Constants.Dapr.MOVER_PUBSUB, WriteStats.Topic)]
    public static Task<IResult> HandleWriteStatsAsync(WriteStats command, ICommandHandler<WriteStats> commandHandler, CancellationToken cancellationToken)
    {
        return CommandDispatcher.DispatchAsync(command, commandHandler, cancellationToken);
    }
}

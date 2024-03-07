using Dapr;
using Mover.FitDecoder.Shared.Commands;
using Mover.Shared;
using Mover.Shared.Dispatchers;
using Mover.Shared.Interfaces;
using Mover.Uploader.Shared.Events;

namespace Mover.FitDecoder;

public static class FitDecoderDispatcher
{
    [Topic(Constants.Dapr.MOVER_PUBSUB, DecodeFit.Topic)]
    public static Task<IResult> HandleDecodeFitAsync(DecodeFit command, ICommandHandler<DecodeFit> commandHandler, CancellationToken cancellationToken)
    {
        return CommandDispatcher.DispatchAsync(command, commandHandler, cancellationToken);
    }

    [Topic(Constants.Dapr.MOVER_PUBSUB, UploadedFit.Topic)]
    public static Task<IResult> HandleUploadedFitAsync(UploadedFit @event, IEventHandler<UploadedFit> eventHandler, CancellationToken cancellationToken)
    {
        return EventDispatcher.DispatchAsync(@event, eventHandler, cancellationToken);
    }
}

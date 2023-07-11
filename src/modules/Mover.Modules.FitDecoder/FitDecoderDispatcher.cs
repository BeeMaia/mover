using Dapr;
using Microsoft.AspNetCore.Http;
using Mover.Modules.FitDecoder.Shared.Commands;
using Mover.Modules.FitDecoder.Shared.Events;
using Mover.Shared;
using Mover.Shared.Dispatchers;
using Mover.Shared.Interfaces;

namespace Mover.Modules.FitDecoder
{
    public static class FitDecoderDispatcher
    {
        [Topic(Constants.Dapr.MOVER_PUBSUB, nameof(DecodeFit))]
        public static Task<IResult> HandleDecodeFitAsync(DecodeFit command, ICommandHandler<DecodeFit> commandHandler, CancellationToken cancellationToken)
        {
            return CommandDispatcher.DispatchAsync(command, commandHandler, cancellationToken);
        }

        [Topic(Constants.Dapr.MOVER_QUEUE, nameof(FitCreated))]
        public static Task<IResult> HandleFitCreatedAsync(FitCreated @event, IEventHandler<FitCreated> eventHandler, CancellationToken cancellationToken)
        {
            return EventDispatcher.DispatchAsync(@event, eventHandler, cancellationToken);
        }
    }
}

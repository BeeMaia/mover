using Dapr;
using Mover.Modules.FitDecoder.Shared.Events;
using Mover.Modules.FitDecoder.Shared.Models;
using Mover.Shared;
using Mover.Shared.Dispatchers;
using Mover.Shared.Interfaces;

namespace Mover
{
    public static class MoverDispatcher
    {
        [Topic(Constants.Dapr.MOVER_PUBSUB, "blobcreated")]
        public static Task<IResult> HandleBlobCreatedAsync(BlobCreated @event, IEventHandler<FitCreated> eventHandler, CancellationToken cancellationToken)
        {
            var fileName = @event.url.Split('/').Last();

            var extension = Path.GetExtension(fileName);
            if (extension.Equals(".fit"))
                return EventDispatcher.DispatchAsync(new FitCreated(fileName), eventHandler, cancellationToken);

            return Task.FromResult(Results.NoContent());
        }
    }
}

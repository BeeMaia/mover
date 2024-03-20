using Mover.Shared.Interfaces;
using Mover.Stats.Shared.Commands;
using Mover.Uploader.Shared.Events;

namespace Mover.Stats.Handlers.Events;

public sealed class UploadedGpxHandler : Mover.Shared.Handlers.EventHandler<UploadedGpx>
{
    private readonly IServiceBus serviceBus;

    public UploadedGpxHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus) : base(loggerFactory)
    {
        this.serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
    }

    public override async Task HandleAsync(UploadedGpx @event, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Handle Event {@Event}", @event);

        await serviceBus.SendAsync(new WriteStats(@event.RawId, @event.FileName, @event.UserId), cancellationToken);
    }
}

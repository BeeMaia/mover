using Mover.FitDecoder.Shared.Commands;
using Mover.Shared.Interfaces;
using Mover.Uploader.Shared.Events;

namespace Mover.FitDecoder.Handlers.Events;

public sealed class UploadedFitHandler : Mover.Shared.Handlers.EventHandler<UploadedFit>
{
    private readonly IServiceBus serviceBus;

    public UploadedFitHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus) : base(loggerFactory)
    {
        this.serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
    }

    public override async Task HandleAsync(UploadedFit @event, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Handle Event {@Event}", @event);

        await serviceBus.SendAsync(new DecodeFit(@event.RawId, @event.FileName, @event.UserId), cancellationToken);
    }
}

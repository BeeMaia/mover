using Microsoft.Extensions.Logging;
using Mover.Modules.FitDecoder.Shared.Commands;
using Mover.Modules.Uploader.Shared.Events;
using Mover.Shared.Interfaces;

namespace Mover.Modules.FitDecoder.Handlers.Events;

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

        await serviceBus.SendAsync(new DecodeFit(@event.RawId, @event.FileName), cancellationToken);
    }
}

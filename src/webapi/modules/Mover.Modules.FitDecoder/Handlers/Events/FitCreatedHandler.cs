using Microsoft.Extensions.Logging;
using Mover.Modules.FitDecoder.Shared.Commands;
using Mover.Modules.FitDecoder.Shared.Events;
using Mover.Shared.Interfaces;

namespace Mover.Modules.FitDecoder.Handlers.Events;

public sealed class FitCreatedHandler : Mover.Shared.Handlers.EventHandler<FitCreated>
{
    private readonly IServiceBus serviceBus;

    public FitCreatedHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus) : base(loggerFactory)
    {
        this.serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
    }

    public override Task HandleAsync(FitCreated @event, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Handle Event {@Event}", @event);

        return serviceBus.SendAsync(new DecodeFit(Guid.NewGuid(), @event.FileName), cancellationToken);
    }
}

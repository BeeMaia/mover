using Microsoft.Extensions.Logging;
using Mover.Modules.FitDecoder.Interfaces;
using Mover.Modules.FitDecoder.Shared.Commands;
using Mover.Modules.FitDecoder.Shared.Events;
using Mover.Shared.Interfaces;

namespace Mover.Modules.FitDecoder.Handlers.Commands;

public sealed class DecodeFitHandler : Mover.Shared.Handlers.CommandHandler<DecodeFit>
{
    private readonly IFitDecoderService fitDecoderService;

    public DecodeFitHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus, IFitDecoderService fitDecoderService) : base(loggerFactory, serviceBus)
    {
        this.fitDecoderService = fitDecoderService ?? throw new ArgumentNullException(nameof(fitDecoderService));
    }

    public override async Task HandleAsync(DecodeFit command, CancellationToken cancellationToken)
    {
        await fitDecoderService.DecodeAsync(command.RawId, command.FileName, cancellationToken).ConfigureAwait(false);
        await ServiceBus.PublishAsync(new FitDecoded(command.RawId), cancellationToken).ConfigureAwait(false);
    }
}

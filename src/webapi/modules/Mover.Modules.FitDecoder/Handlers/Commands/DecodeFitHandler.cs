using Microsoft.Extensions.Logging;
using Mover.Modules.FitDecoder.Interfaces;
using Mover.Modules.FitDecoder.Shared.Commands;
using Mover.Modules.Uploader.Shared.Events;
using Mover.Shared;
using Mover.Shared.Extensions;
using Mover.Shared.Interfaces;

namespace Mover.Modules.FitDecoder.Handlers.Commands;

public sealed class DecodeFitHandler : Mover.Shared.Handlers.CommandHandler<DecodeFit>
{
    private readonly IFitDecoderService fitDecoderService;
    private readonly IBlobRepository blobRepository;

    public DecodeFitHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus, IFitDecoderService fitDecoderService, IBlobRepository blobRepository) : base(loggerFactory, serviceBus)
    {
        this.fitDecoderService = fitDecoderService ?? throw new ArgumentNullException(nameof(fitDecoderService));
        this.blobRepository = blobRepository;
    }

    public override async Task HandleAsync(DecodeFit command, CancellationToken cancellationToken)
    {
        var gpx = await fitDecoderService.DecodeAsync(command.RawId, command.FileName, cancellationToken).ConfigureAwait(false);
        var gpxFileName = $"{Path.GetFileNameWithoutExtension(command.FileName)}{Constants.Extension.GPX}";

        await blobRepository.CreateBlobAsync(Constants.Dapr.MOVER_GPXBLOB, gpxFileName, gpx.ToArray(), cancellationToken).ConfigureAwait(false);

        await ServiceBus.PublishAsync(new UploadedGpx(gpxFileName, command.RawId), cancellationToken).ConfigureAwait(false);
    }
}

using Microsoft.Extensions.Logging;
using Mover.Modules.Uploader.Interfaces;
using Mover.Modules.Uploader.Shared.Commands;
using Mover.Shared;
using Mover.Shared.Interfaces;

namespace Mover.Modules.Uploader.Handlers.Commands;

public sealed class UploadFileHandler : Mover.Shared.Handlers.CommandHandler<UploadFile>
{
    private readonly IUploaderService uploaderService;
    private readonly IBlobRepository blobRepository;

    public UploadFileHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus, IUploaderService uploaderService, IBlobRepository blobRepository) : base(loggerFactory, serviceBus)
    {
        this.uploaderService = uploaderService ?? throw new ArgumentNullException(nameof(uploaderService));
        this.blobRepository = blobRepository;
    }

    public override async Task HandleAsync(UploadFile command, CancellationToken cancellationToken)
    {
        var content = await blobRepository.GetBlobAsync(Constants.Dapr.MOVER_RAWBLOB, command.RawId.ToString(), cancellationToken).ConfigureAwait(false);

        var uploadedEvent = await uploaderService.UploadAsync(command.RawId, command.FileName, content, cancellationToken).ConfigureAwait(false);
        if (uploadedEvent != null)
        {
            await ServiceBus.PublishAsync(uploadedEvent, cancellationToken).ConfigureAwait(false);
        }
    }
}
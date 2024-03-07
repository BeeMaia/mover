using Mover.Shared;
using Mover.Shared.Handlers;
using Mover.Shared.Interfaces;
using Mover.Uploader.Interfaces;
using Mover.Uploader.Shared.Commands;

namespace Mover.Uploader.Handlers.Commands;

public sealed class UploadFileHandler : CommandHandler<UploadFile>
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
        var content = await blobRepository.GetBlobAsync(Constants.Dapr.MOVER_RAWBLOB, command.FileName, cancellationToken);

        var uploadedEvent = await uploaderService.UploadAsync(command.RawId, command.FileName, content, cancellationToken);
        if (uploadedEvent != null)
        {
            await ServiceBus.PublishAsync(uploadedEvent, cancellationToken);
        }
    }
}
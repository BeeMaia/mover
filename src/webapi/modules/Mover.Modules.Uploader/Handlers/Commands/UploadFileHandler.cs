using Microsoft.Extensions.Logging;
using Mover.Modules.Uploader.Interfaces;
using Mover.Modules.Uploader.Shared.Commands;
using Mover.Shared.Interfaces;

namespace Mover.Modules.Uploader.Handlers.Commands;

public sealed class UploadFileHandler : Mover.Shared.Handlers.CommandHandler<UploadFile>
{
    private readonly IUploaderService uploaderService;

    public UploadFileHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus, IUploaderService uploaderService) : base(loggerFactory, serviceBus)
    {
        this.uploaderService = uploaderService ?? throw new ArgumentNullException(nameof(uploaderService));
    }

    public override async Task HandleAsync(UploadFile command, CancellationToken cancellationToken)
    {
        var uploadedEvent = await uploaderService.UploadAsync(command.FileName, command.Content, cancellationToken).ConfigureAwait(false);
        if (uploadedEvent != null)
        {
            await ServiceBus.PublishAsync(uploadedEvent, cancellationToken).ConfigureAwait(false);
        }
    }
}
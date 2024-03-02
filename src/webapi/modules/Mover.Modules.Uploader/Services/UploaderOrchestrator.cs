using Microsoft.AspNetCore.Http;
using Mover.Modules.Uploader.Shared.Commands;
using Mover.Shared;
using Mover.Shared.Interfaces;

namespace Mover.Modules.Uploader.Services;

public class UploaderOrchestrator
{
    private readonly IServiceBus serviceBus;
    private readonly IBlobRepository blobRepository;

    public UploaderOrchestrator(IServiceBus serviceBus, IBlobRepository blobRepository)
    {
        this.serviceBus = serviceBus;
        this.blobRepository = blobRepository;
    }

    public async Task UploadAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var rawId = Guid.NewGuid();
        var fileName = $"{rawId}_{file.FileName}";

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken).ConfigureAwait(false);
        stream.Position = 0;
        var content = stream.ToArray();

        await blobRepository.CreateBlobAsync(Constants.Dapr.MOVER_RAWBLOB, rawId.ToString(), content, cancellationToken).ConfigureAwait(false);

        await serviceBus.SendAsync(new UploadFile(rawId, fileName), cancellationToken).ConfigureAwait(false);
    }
}
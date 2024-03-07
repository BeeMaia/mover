using Mover.Shared;
using Mover.Shared.Interfaces;
using Mover.Uploader.Shared.Commands;

namespace Mover.Uploader.Services;

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
        await file.CopyToAsync(stream, cancellationToken);
        stream.Position = 0;
        var content = stream.ToArray();

        await blobRepository.CreateBlobAsync(Constants.Dapr.MOVER_RAWBLOB, fileName, content, cancellationToken);

        await serviceBus.SendAsync(new UploadFile(rawId, fileName), cancellationToken);
    }
}
using Mover.Shared;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;
using Mover.Uploader.Interfaces;
using Mover.Uploader.Shared.Events;

namespace Mover.Uploader.Strategies;

public class GpxStrategy : IUploadStrategy
{
    private readonly IBlobRepository blobRepository;

    public GpxStrategy(IBlobRepository blobRepository)
    {
        this.blobRepository = blobRepository;
    }

    public string Extension => Constants.Extension.GPX;

    public async Task<Event> UploadAsync(Guid rawId, string fileName, byte[] content, CancellationToken cancellationToken)
    {
        await blobRepository.CreateBlobAsync(Constants.Dapr.MOVER_GPXBLOB, fileName, content, cancellationToken);
        return new UploadedGpx(fileName, rawId);
    }
}
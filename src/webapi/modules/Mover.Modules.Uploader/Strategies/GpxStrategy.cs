using Mover.Modules.Uploader.Interfaces;
using Mover.Modules.Uploader.Shared.Events;
using Mover.Shared;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;

namespace Mover.Modules.Uploader.Strategies;

public class GpxStrategy : IUploadStrategy
{
    private readonly IBlobRepository blobRepository;

    public GpxStrategy(IBlobRepository blobRepository)
    {
        this.blobRepository = blobRepository;
    }

    public string Extension => Constants.Extension.GPX;

    public async Task<Event> UploadAsync(string fileName, byte[] content, CancellationToken cancellationToken)
    {
        await blobRepository.CreateBlobAsync(Constants.Dapr.MOVER_GPXBLOB, fileName, content, cancellationToken).ConfigureAwait(false);
        return new UploadedGpx(fileName);
    }
}
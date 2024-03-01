using Mover.Modules.Uploader.Interfaces;
using Mover.Modules.Uploader.Shared.Events;
using Mover.Shared;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;

namespace Mover.Modules.Uploader.Strategies;

public class FitStrategy : IUploadStrategy
{
    private readonly IBlobRepository blobRepository;

    public FitStrategy(IBlobRepository blobRepository)
    {
        this.blobRepository = blobRepository;
    }

    public string Extension => Constants.Extension.FIT;

    public async Task<Event> UploadAsync(string fileName, byte[] content, CancellationToken cancellationToken)
    {
        await blobRepository.CreateBlobAsync(Constants.Dapr.MOVER_FITBLOB, fileName, content, cancellationToken).ConfigureAwait(false);
        return new UploadedFit(fileName);
    }
}

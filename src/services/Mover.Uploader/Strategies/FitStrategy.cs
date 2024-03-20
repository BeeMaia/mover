using Mover.Shared;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;
using Mover.Uploader.Interfaces;
using Mover.Uploader.Shared.Events;

namespace Mover.Uploader.Strategies;

public class FitStrategy : IUploadStrategy
{
    private readonly IBlobRepository blobRepository;

    public FitStrategy(IBlobRepository blobRepository)
    {
        this.blobRepository = blobRepository;
    }

    public string Extension => Constants.Extension.FIT;

    public async Task<Event> UploadAsync(Guid rawId, string fileName, string userId, byte[] content, CancellationToken cancellationToken)
    {
        await blobRepository.CreateBlobAsync(Constants.Dapr.MOVER_FITBLOB, fileName, content, cancellationToken);
        return new UploadedFit(fileName, rawId, userId);
    }
}

using Mover.Shared.Models;
using Mover.Uploader.Interfaces;
using Mover.Uploader.Strategies;

namespace Mover.Uploader.Services;

public class UploaderService : IUploaderService
{
    private readonly UploadContext uploadContext;

    public UploaderService(UploadContext uploadContext)
    {
        this.uploadContext = uploadContext;
    }

    public async Task<Event?> UploadAsync(Guid rawId, string fileName, string userId, byte[] content, CancellationToken cancellationToken)
    {
        return await uploadContext.UploadAsync(rawId, fileName, userId, content, cancellationToken);
    }
}
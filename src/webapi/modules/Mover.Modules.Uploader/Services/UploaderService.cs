using Mover.Modules.Uploader.Interfaces;
using Mover.Modules.Uploader.Strategies;
using Mover.Shared.Models;

namespace Mover.Modules.Uploader.Services;

public class UploaderService : IUploaderService
{
    private readonly UploadContext uploadContext;

    public UploaderService(UploadContext uploadContext)
    {
        this.uploadContext = uploadContext;
    }

    public async Task<Event?> UploadAsync(Guid rawId, string fileName, byte[] content, CancellationToken cancellationToken)
    {
        return await uploadContext.UploadAsync(rawId, fileName, content, cancellationToken);
    }
}
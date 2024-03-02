using Mover.Shared.Models;

namespace Mover.Modules.Uploader.Interfaces;

public interface IUploaderService
{
    Task<Event?> UploadAsync(Guid rawId, string fileName, byte[] content, CancellationToken cancellationToken);
}

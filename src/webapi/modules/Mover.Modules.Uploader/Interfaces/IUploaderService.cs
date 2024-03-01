using Mover.Shared.Models;

namespace Mover.Modules.Uploader.Interfaces;

public interface IUploaderService
{
    Task<Event?> UploadAsync(string fileName, byte[] content, CancellationToken cancellationToken);
}

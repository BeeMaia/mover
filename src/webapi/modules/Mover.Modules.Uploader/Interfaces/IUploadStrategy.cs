using Mover.Shared.Models;

namespace Mover.Modules.Uploader.Interfaces;

public interface IUploadStrategy
{
    public string Extension { get; }
    Task<Event> UploadAsync(string fileName, byte[] content, CancellationToken cancellationToken);
}

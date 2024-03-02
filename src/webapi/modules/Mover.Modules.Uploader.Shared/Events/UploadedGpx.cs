using Mover.Shared.Models;

namespace Mover.Modules.Uploader.Shared.Events;

public record UploadedGpx : Event
{
    public string FileName { get; }
    public Guid RawId { get; }

    public UploadedGpx(string fileName, Guid rawId)
    {
        FileName = fileName;
        RawId = rawId;
    }
}
using Mover.Shared.Models;

namespace Mover.Modules.Uploader.Shared.Events;
public record UploadedFit : Event
{
    public Guid RawId { get; }
    public string FileName { get; }

    public UploadedFit(string fileName, Guid rawId)
    {
        FileName = fileName;
        RawId = rawId;
    }
}
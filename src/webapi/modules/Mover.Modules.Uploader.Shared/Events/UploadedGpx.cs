using Mover.Shared.Models;

namespace Mover.Modules.Uploader.Shared.Events;

public record UploadedGpx : Event
{
    public string FileName { get; }

    public UploadedGpx(string fileName)
    {
        FileName = fileName;
    }
}
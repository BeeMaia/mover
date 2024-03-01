using Mover.Shared.Models;

namespace Mover.Modules.Uploader.Shared.Events;
public record UploadedFit : Event
{
    public string FileName { get; }

    public UploadedFit(string fileName)
    {
        FileName = fileName;
    }
}
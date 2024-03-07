using Mover.Shared.Models;

namespace Mover.Uploader.Shared.Events;

public record UploadedGpx : Event
{
    public const string Topic = "uploadedgpx";

    public string FileName { get; }
    public Guid RawId { get; }

    public UploadedGpx(string fileName, Guid rawId)
    {
        FileName = fileName;
        RawId = rawId;
    }
}
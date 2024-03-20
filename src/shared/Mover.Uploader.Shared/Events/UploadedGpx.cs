using Mover.Shared.Models;

namespace Mover.Uploader.Shared.Events;

public record UploadedGpx : Event
{
    public const string Topic = "uploadedgpx";

    public string FileName { get; }
    public Guid RawId { get; }
    public string UserId { get; set; }

    public UploadedGpx(string fileName, Guid rawId, string userId)
    {
        FileName = fileName;
        RawId = rawId;
        UserId = userId;
    }
}
using Mover.Shared.Models;

namespace Mover.Uploader.Shared.Events;
public record UploadedFit : Event
{
    public const string Topic = "uploadedfit";
    public Guid RawId { get; }
    public string FileName { get; }
    public string UserId { get; }

    public UploadedFit(string fileName, Guid rawId, string userId)
    {
        FileName = fileName;
        RawId = rawId;
        UserId = userId;
    }
}
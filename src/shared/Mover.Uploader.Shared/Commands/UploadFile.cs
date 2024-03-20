using Mover.Shared.Models;

namespace Mover.Uploader.Shared.Commands;

public record UploadFile : Command
{
    public const string Topic = "uploadfile";
    public string FileName { get; }
    public string UserId { get; }
    public Guid RawId { get; }

    public UploadFile(Guid rawId, string fileName, string userId)
    {
        FileName = fileName;
        RawId = rawId;
        UserId = userId;
    }
}
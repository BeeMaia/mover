using Mover.Shared.Models;

namespace Mover.Uploader.Shared.Commands;

public record UploadFile : Command
{
    public const string Topic = "uploadfile";
    public string FileName { get; }
    public Guid RawId { get; }

    public UploadFile(Guid rawId, string fileName)
    {
        FileName = fileName;
        RawId = rawId;
    }
}
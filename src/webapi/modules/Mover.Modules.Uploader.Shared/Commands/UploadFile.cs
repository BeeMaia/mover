using Mover.Shared.Models;

namespace Mover.Modules.Uploader.Shared.Commands;

public record UploadFile : Command
{
    public string FileName { get; }
    public Guid RawId { get; }

    public UploadFile(Guid rawId, string fileName)
    {
        FileName = fileName;
        RawId = rawId;
    }
}
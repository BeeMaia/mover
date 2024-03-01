using Mover.Shared.Models;

namespace Mover.Modules.Uploader.Shared.Commands;

public record UploadFile : Command
{
    public string FileName { get; }
    public byte[] Content { get; }

    public UploadFile(string fileName, byte[] content)
    {
        FileName = fileName;
        Content = content;
    }
}
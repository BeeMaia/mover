using Mover.Shared.Models;

namespace Mover.Modules.FitDecoder.Shared.Commands;

public record DecodeFit : Command
{
    public Guid RawId { get; }

    public string FileName { get; }

    public DecodeFit(Guid rawId, string fileName)
    {
        RawId = rawId;
        FileName = fileName;
    }
}
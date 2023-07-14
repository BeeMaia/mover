using Mover.Shared.Models;

namespace Mover.Modules.FitDecoder.Shared.Events;

public record FitDecoded : Event
{
    public Guid RawId { get; }
    public string FileName { get; }

    public FitDecoded(Guid rawId, string fileName)
    {
        RawId = rawId;
        FileName = fileName;
    }
}
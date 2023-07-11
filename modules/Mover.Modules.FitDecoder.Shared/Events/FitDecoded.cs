using Mover.Shared.Models;

namespace Mover.Modules.FitDecoder.Shared.Events;

public record FitDecoded : Event
{
    public Guid RawId { get; }

    public FitDecoded(Guid rawId)
    {
        RawId = rawId;
    }
}
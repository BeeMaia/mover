using Mover.Shared.Models;

namespace Mover.FitDecoder.Shared.Events;

public record FitDecoded : Event
{
    public Guid RawId { get; }
    public string FileName { get; }
    public string UserId { get; }

    public FitDecoded(Guid rawId, string fileName, string userId)
    {
        RawId = rawId;
        FileName = fileName;
        UserId = userId;
    }
}